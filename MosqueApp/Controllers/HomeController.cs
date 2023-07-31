using Microsoft.AspNetCore.Mvc;
using MosqueApp.Models;
using MosqueApp.Models.DataContext;
using MosqueApp.Models.Model;
using System.Diagnostics;

namespace MosqueApp.Controllers
{
    public class HomeController : Controller
    {
        MosqueContext c = new MosqueContext();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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