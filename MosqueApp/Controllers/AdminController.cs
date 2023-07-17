using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MosqueApp.Controllers
{
    public class AdminController : Controller
    {
      
        public IActionResult Index()
        {
                return View();
        }


    }
}
