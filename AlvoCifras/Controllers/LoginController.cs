﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AlvoCifras.Models;
using AlvoCifras.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AlvoCifras.Controllers
{
    public class LoginController : Controller
    {
        private Context _context;
        public LoginController(Context context)
        {
            _context = context;
        }

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

                    return RedirectToAction("Index", "Admin");

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