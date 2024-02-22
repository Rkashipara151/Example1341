using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Employee.Models;
using Employee.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Employee.Controllers
{
    // [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;

        private IRegisterRepositories _registerRepositories;

        public RegisterController(ILogger<RegisterController> logger, IRegisterRepositories registerRepositories)
        {
            _logger = logger;
            _registerRepositories = registerRepositories;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CheckEmail(string email)
        {
            bool emailExists = _registerRepositories.CheckEmailExists(email);
            return Json(!emailExists); // Return true if email does NOT exist, enabling form submission
        }

        [HttpPost]

        public IActionResult Register(RegisterModel register)
        {
            _registerRepositories.Insert(register);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

     [HttpPost]
public IActionResult Login(RegisterModel login)
{
    var (user, IsAdmin) = _registerRepositories.login(login);
    if (user != null) // Check if user is found
    {
        if (IsAdmin)
        {
            return RedirectToAction("Index", "Home"); // Redirect to admin dashboard
        }
        else
        {
            return RedirectToAction("Detail", "Employee"); // Redirect to detail page for regular user
        }
    }
    else
    {
        // Handle invalid login credentials
        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return View(login);
    }
      }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}