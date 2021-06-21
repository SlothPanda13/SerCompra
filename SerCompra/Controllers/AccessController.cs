using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SerCompra.Models.DataBase;
using SerCompra.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;




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
                using (var db = new sercompraContext())
                {
                    var oUser = db.Usuarios.Where (d=> d.Email == email).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.RecoveryToken = token;
                        db.Entry(oUser).State = EntityState.Modified;
                        db.SaveChanges();

                        //enviar mail
                        SendEmail(oUser.Email,token);
                    }
                    else {
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
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string EmailDestino,string token)
        {
            string EmailOrigen = "SercompraSupport@gmail.com";
            string Contraseña = "Support12345sercompra";
            string url = urlDomain+"/Access/Recovery/?token="+token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de contraseña",
                "<p>Hemos recibido una solicitud para recuperar su contraseña, haga click en el siguiente enlace para recuperarla</p><br>"+
                     "<a href='"+url+"'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }

        #endregion
    }
}