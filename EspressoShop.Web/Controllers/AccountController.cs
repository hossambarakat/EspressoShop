using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EspressoShop.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EspressoShop.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View();

            var claimsIdentity = AuthenticateUser(model.Email, model.Password);

            if (claimsIdentity == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());

            _logger.LogInformation($"User {model.Email} logged in at {DateTime.UtcNow}.");

            return Redirect(returnUrl ?? "/");

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"User {User.Identity.Name} logged out at {DateTime.UtcNow}.");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }

        private ClaimsIdentity AuthenticateUser(string email, string password)
        {
            // For demonstration purposes, authenticate a user 
            // with a static email address. Ignore the password.

            if (email == "admin@example.com")
            {
                var claims = new List<Claim>
                {
                    new Claim( ClaimTypes.Name, "Administrator"),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, "Administrator")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                return claimsIdentity;
            }
            if (email == "hossam@example.com")
            {
                var claims = new List<Claim>
                {
                    new Claim( ClaimTypes.Name, "Hossam Barakat"),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                return claimsIdentity;
            }

            return null;
        }
    }
}
