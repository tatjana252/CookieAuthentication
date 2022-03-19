using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<Person> manager;
        private readonly SignInManager<Person> signInManager;

        public AuthenticationController(UserManager<Person> manager, SignInManager<Person> signInManager)
        {
            this.manager = manager;
            this.signInManager = signInManager;
        }

        #region registracija
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel register)
        {
            Person person = new Person
            {
                Email = register.Email,
                UserName = register.Username,
                FirstName = register.FirstName,
                LastName = register.LastName,
            };
            var result = await manager.CreateAsync(person, register.Password);


            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if(result.Errors.Any(e => e.Code == "DuplicateUserName"))
                ModelState.AddModelError("Username", result.Errors.FirstOrDefault(e => e.Code == "DuplicateUserName")?.Description);
                if (result.Errors.Any(e => e.Code.Contains("Password")))
                ModelState.AddModelError("Password", result.Errors.FirstOrDefault(e => e.Code.Contains("Password"))?.Description);
                return View();
            }
        }
        #endregion

        #region prijava na sistem
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel login)
        {
            var result =  await signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #endregion
    }
}
