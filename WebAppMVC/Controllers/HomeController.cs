using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller //svim endpoint-ovima u ovom kontrolenu mogu da pristupe samo autentifikovani korisnici
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            var user = HttpContext.User;
            return View();
        }
        //[AllowAnonymous]
        [Authorize(Roles = "Teacher")]
        public IActionResult Privacy() //ovoj stranici moze da pristupi samo nastavnik
        {
            var user = HttpContext.User;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
