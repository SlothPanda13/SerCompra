using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerCompra.Models.DataBase;

namespace SerCompra.Controllers
{
    public class AccessController : Controller
    {
        string urlDomain = "http://localhost:5001/";

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string email, string password)
        {
            try
            {
                using (var db = new SercompraContext())
                {
                    /*var usr = from d in db.Usuarios
                        where d.Email == email && d.Contraseña == password
                        select d;*/
                    var rl = from d in db.Usuarios
                        where (d.Email == email && d.Contraseña == password)
                        select d.Rol;
                    if (rl.Count() > 0)
                    {
                        //Session["User"] = rl.First();
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
                    else
                    {
                        ViewBag.Error = "Usuario o contraseña incorrectos";
                        return View();
                    }
                }
            }
            catch (Exception e)
            {
                return Content("Ocurrió un error " + e.Message);
            }

            //return View("Login");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Recovery(string email)
        {
            try
            {
                string token = GetSha256(Guid.NewGuid().ToString());
                using (var db = new SercompraContext())
                {
                    var oUser = db.Usuarios.Where(d => d.Email == email).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.RecoveryToken = token;
                        db.Entry(oUser).State = EntityState.Modified;
                        db.SaveChanges();

                        //enviar mail
                        SendEmail(oUser.Email, token);
                    }
                    else
                    {
                        ViewBag.Error = "El usuario no existe";
                        return View("Login");
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPass()
        {
            return View();
        }

        #region HELPERS

        private string GetSha256(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] stream = sha256.ComputeHash(Encoding.ASCII.GetBytes(str));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in stream)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "SercompraSupport@gmail.com";
            string Contraseña = "Support12345sercompra";
            string url = urlDomain + "/Access/Recovery/?token=" + token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de contraseña",
                "<p>Hemos recibido una solicitud para recuperar su contraseña, haga click en el siguiente enlace para recuperarla</p><br>" +
                "<a href='" + url + "'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }

        #endregion
    }
}