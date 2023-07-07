using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TESTEMPLOYEE.Models;
using TESTEMPLOYEE.DBA;

namespace TESTEMPLOYEE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DataAccess _dba;
        public HomeController(ILogger<HomeController> logger, IConfiguration conf)
        {
            _dba = new DataAccess(conf); 
            _logger = logger;
        }

        public IActionResult Employee()
        {
            List<Employee> lst = _dba.GetEmployee();
            return View(lst);
        }


        public IActionResult Contractor()
        {
            List<Contractor> lst = _dba.GetContractor();
            return View(lst);
        }


        public IActionResult EditContractor(int id)
        {
            Contractor data = _dba.GetContractor().Where(p => p.ContractorId == id).FirstOrDefault();

            return View(data);

        }

        [HttpPost]
        public IActionResult EditContractor(Contractor c)
        {
            bool inserted = _dba.UpdateContractor(c);
            if (inserted)
            {
                return RedirectToAction("Contractor");
            }
            
            return View("Contractor");

        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}