using EESTEC.Interfaces;
using EESTEC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EESTEC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        public IActionResult AboutPartial()
        {
            return PartialView("_About");
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}