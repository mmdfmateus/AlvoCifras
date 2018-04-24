using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AlvoCifras.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AlvoCifras.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private bool LoginUser(string userName, string password)
        {
            return userName == "admin" && password == "alvo123";
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (LoginUser(loginViewModel.UserName, loginViewModel.Password))
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, loginViewModel.UserName) };
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Home");

                }
            }

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();

            return View("Index");
        }
    }
}