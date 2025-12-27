using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SerCompra.Services;

namespace SerCompra.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        string urlDomain = "http://localhost:5001/";

        public AccessController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var user = await _authService.ValidateUserAsync(email, password);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Rol)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Usuario o contraseña incorrectos";
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Por favor intente nuevamente.";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Recovery(string email)
        {
            try
            {
                var user = await _authService.GetUserByEmailAsync(email);
                if (user != null)
                {
                    string token = await _authService.GenerateRecoveryTokenAsync(user);
                    string url = $"{urlDomain}/Access/Recovery/?token={token}";
                    string body =
                        "<p>Hemos recibido una solicitud para recuperar su contraseña, haga click en el siguiente enlace para recuperarla</p><br>" +
                        $"<a href='{url}'>Click para recuperar</a>";

                    await _emailService.SendEmailAsync(user.Email, "Recuperación de contraseña", body);

                    return RedirectToAction("Privacy", "Home"); // Should be a "Check your email" page
                }
                else
                {
                    ViewBag.Error = "El usuario no existe";
                    return View("Login");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error al procesar su solicitud.";
                return View("Login");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPass()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPass(string token,
            [FromForm(Name = "reset-password-new")] string newPassword,
            [FromForm(Name = "reset-password-confirm")] string confirmPassword)
        {
            try
            {
                if (newPassword != confirmPassword)
                {
                    ViewBag.Error = "Las contraseñas no coinciden.";
                    return View();
                }

                var user = await _authService.GetUserByRecoveryTokenAsync(token);
                if (user != null)
                {
                    await _authService.ResetPasswordAsync(user, newPassword);

                    ViewBag.Message = "Contraseña restablecida exitosamente.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Error = "Token inválido o expirado.";
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error al restablecer la contraseña.";
                return View();
            }
        }
    }
}