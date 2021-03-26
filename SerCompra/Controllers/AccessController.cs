using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerCompra.Models.DataBase;
using SerCompra.Models;
using Microsoft.AspNetCore.Http;

namespace SerCompra.Controllers
{
    public class AccessController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Entrar(string email, string password)
        {
            try
            {
                using (var db = new sercompraContext())
                {
                    /*var usr = from d in db.Usuarios
                        where d.Email == email && d.Contraseña == password
                        select d;*/
                    var rl = from d in db.Usuarios
                        where (d.Email == email && d.Contraseña == password)
                        select d.Rol;
                    if (rl.Count() > 0)

                    {
                        if (rl.Contains("Administrador"))
                        {
                            return RedirectToAction("index", "Home");
                        }
                        if (rl.Contains("Funcionario"))
                        {
                            return RedirectToAction("index", "Home");
                        }
                        if (rl.Contains("Proveedor"))
                        {
                            return RedirectToAction("index", "Home");
                        }

                        
                    }
                    else {
                        return Content("Usuario y/o contraseña invalidos");
                    }
                }
            }
            catch (Exception e)
            {
                return Content("Ocurrió un error " + e.Message);
            }
            //return View("Login");
            return RedirectToAction("Privacy", "Home");

        }
    }
}