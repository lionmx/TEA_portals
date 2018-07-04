using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backoffice0._1.Models;
using Backoffice0._1.Helper;
using Backoffice0._1.Controllers.POS;

namespace Backoffice0._1.Controllers
{
    public class USUARIOLOGINController : Controller
    {

        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        List<PermisosUsuario> permisos; 
        List<SubmodulosUsuario> submodulos;
        List<ModulosUsuario> modulos;
        List<string> sucursales_asignadas;
        string codigo_sucursal;
        //GET: USUARIOLOGIN
        public ActionResult UsuarioLogin()
        {

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioLogin(CS_usuario_login uSUARIO_LOGIN)
        {

            var encodingPasswordString = string.Empty;
            var items = db.C_usuarios_corporativo.Where(u => u.usuario.Equals(uSUARIO_LOGIN.CS_usuarios.NOMBRE));

            if (items.Count()>0)
            {
                if (uSUARIO_LOGIN.PASS != null)
                {
                    encodingPasswordString = PasswordHelper.EncodePassword(uSUARIO_LOGIN.PASS, "MySalt");
                }

                foreach (var n in items)
                {
                    if (n.password != null)
                    {
                        if (uSUARIO_LOGIN.CS_usuarios.NOMBRE.Equals(n.usuario) && n.password.Equals(encodingPasswordString))
                        {
                            
                            Session["LoggedUser"] = n.usuario;
                            Session["LoggedId"] = n.id_usuario_corporativo;
                            Session["LoggedIdRol"] = n.id_rol;
                            ViewBag.idusuario = n.id_usuario_corporativo;
                            ViewBag.compras = 0;
                            Session["LoggedTipoUsuario"] = n.id_usuario_tipo;
                            var sucursales = from s in db.C_usuarios_sucursales
                                             where s.id_usuario_corporativo == n.id_usuario_corporativo
                                             select s;
                            sucursales_asignadas = new List<string>();
                            if (sucursales.Count() > 0)
                            {
                                foreach (var item in sucursales)
                                {
                                    sucursales_asignadas.Add(item.codigo_sucursal);
                                }
                                Session["LoggedUserSucursales"] = sucursales_asignadas;
                            }
                            foreach (var item in Session["LoggedUserSucursales"] as List<string>)
                            {
                                codigo_sucursal = item;
                            }
                            Session["codigo_sucursal"] = codigo_sucursal;
                            PEDIDOSController pc = new PEDIDOSController();
                            int id_marca = pc.ConsultarMarcaPrincipal((string)Session["codigo_sucursal"]);
                            Session["id_marca"] = id_marca;
                            var logo_marca = from m in db.C_marcas_g
                                             where m.id_marca == id_marca
                                             select m;
                            foreach(var item in logo_marca)
                            {
                                Session["logo_marca"] = item.logo;
                            }

                            uSUARIO_LOGIN.ID_USUARIO = n.id_usuario_corporativo;
                            uSUARIO_LOGIN.PASS = encodingPasswordString;
                            uSUARIO_LOGIN.FECHA_LOGIN = DateTime.Now.ToString();
                        }
                        else
                        {
                            ViewBag.Message = "Usuario o contraseña incorrectos";
                            return View("UsuarioLogin");

                        }
                    }
                }
            }
            else
            {
                ViewBag.Message = "Usuario o contraseña incorrectos";
                return View("UsuarioLogin");
            }
            if (ModelState.IsValid)
            {
                //obtiene los permisos de cada servicio/modulo para el usuario loggeado           
                List<int> permisosLista = new List<int>();
                int RolId = (int)Session["LoggedIdRol"];
                var permisosServicioModulo = from p in db.C_modulos_sub_permisos
                                             where p.id_rol == RolId && p.estatus == true
                                             select p;

                if (permisosServicioModulo.Count()>0)
                {
                    permisos = new List<PermisosUsuario>();
                    submodulos = new List<SubmodulosUsuario>();
                    modulos = new List<ModulosUsuario>();
                    foreach (var n in permisosServicioModulo)
                    {
                        permisosLista.Add((int)n.id_modulos_sub);
                        permisosLista.Add((int)n.C_modulos_sub.id_modulo);
                        permisosLista.Add(Convert.ToInt32(n.id_permiso));
                        permisos.Add(new PermisosUsuario((int)n.C_modulos_sub.id_modulo,n.C_modulos_sub.C_modulos.nombre, (int)n.id_modulos_sub, (int)n.id_permiso));
                    }
                    foreach (var item in permisosServicioModulo.Select(m => new {m.C_modulos_sub.id_modulo, m.id_modulos_sub, m.C_modulos_sub.nombre, m.C_modulos_sub.funcion, m.C_modulos_sub.controlador, m.C_modulos_sub.parametros }).Distinct())
                    {
                            submodulos.Add(new SubmodulosUsuario((int)item.id_modulo,(int)item.id_modulos_sub, item.nombre,item.funcion, item.controlador, item.parametros));
                    }
                    foreach (var item in permisosServicioModulo.Select(m => new { m.C_modulos_sub.id_modulo, m.C_modulos_sub.C_modulos.nombre, m.C_modulos_sub.C_modulos.icono }).Distinct())
                    {
                            modulos.Add(new ModulosUsuario((int)item.id_modulo, item.nombre, item.icono));
                    }
                }
                
                Session["modulos"] = modulos;
                Session["submodulos"] = submodulos;
               
                ViewBag.permisos = permisosLista;
                CS_usuario_login obj = new CS_usuario_login();
                obj.ID_USUARIO = uSUARIO_LOGIN.ID_USUARIO;
                obj.PASS = uSUARIO_LOGIN.PASS;
                obj.FECHA_LOGIN = uSUARIO_LOGIN.FECHA_LOGIN;
                db.CS_usuario_login.Add(obj);
                db.SaveChanges();

                if ((int)Session["LoggedIdRol"] == 6 || (int)Session["LoggedIdRol"] == 28)
                {
                    return RedirectToAction("Index", "POS");
                }
                else
                {
                    return View("/Views/Home/Index.cshtml");

                }

            }

            ViewBag.Message = "Usuario o contraseña incorrectos";
            return RedirectToAction("UsuarioLogin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ConsultarModulos(int id_rol)
        {
            var modulos = from m in db.C_modulos_sub_permisos
                          where m.id_rol == id_rol && m.estatus == true
                          select m;
                          
            return PartialView("_VisualizaModulos",modulos);
        }
    }
}