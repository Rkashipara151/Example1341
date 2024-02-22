// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Employee.Models;
// using Employee.Repositories;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;

// namespace Employee.Controllers
// {
//     // [Route("[controller]")]
//     public class EmployeeController : Controller
//     {
//         private readonly ILogger<EmployeeController> _logger;

//         private IEmployeeRepositories _empRepositories;

//         public EmployeeController( IEmployeeRepositories empRepositories)
//         {

//             _empRepositories = empRepositories;
//         }

//         public IActionResult Index()
//         {
//             return View();
//         }

//         public IActionResult Detail()
//         {
//            List<EmployeeModel> crud = _empRepositories.GetAll();

//             return View(crud);

//         }

//         public IActionResult Register()
//         {
//             return View();
//         }

//         [HttpPost]
//         public IActionResult Register(EmployeeModel register)
//         {
//             _empRepositories.Insert(register);
//             return RedirectToAction("Detail");
//         }

//         public IActionResult Update(int id)
//         {
//             EmployeeModel res = _empRepositories.GetOne(id);
//             return View(res);
//         }

//         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//         public IActionResult Error()
//         {
//             return View("Error!");
//         }
//     }
// }


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
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private IEmployeeRepositories _empRepositories;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepositories empRepositories)
        {
            _logger = logger;
            _empRepositories = empRepositories;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            List<EmployeeModel> crud = _empRepositories.GetAll();
            return View(crud);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(EmployeeModel register)
        {
            _empRepositories.Insert(register);
            return RedirectToAction("Detail");
        }

        public IActionResult Update(int id)
        {
            EmployeeModel res = _empRepositories.GetOne(id);
            return View(res);
        }

        [HttpPost]
        public IActionResult Update(EmployeeModel employee)
        {
            _empRepositories.Update(employee);
            return RedirectToAction("Detail");
        }

        public IActionResult Delete(int id)
        {
            _empRepositories.Delete(id);
            return RedirectToAction("Detail");
        }
        public IActionResult Payroll(int id)
        {
            EmployeeModel res = _empRepositories.GetOne(id);

            var grossSalary = res.c_gsalary;
            res.c_basic = grossSalary * 0.60;
            res.c_da = grossSalary * 0.25;
            res.c_hra = grossSalary * 0.15;
            var taxableAmount = grossSalary > 25000 ? grossSalary - 25000 : 0;
            res.c_tax = taxableAmount * 0.10;
            res.c_takehome = grossSalary - res.c_tax;
            res.c_taxable = taxableAmount;

            _empRepositories.UpdateSalary(res);
            return View(res);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
