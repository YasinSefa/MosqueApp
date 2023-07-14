using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MosqueApp.Controllers
{
    public class AdminController : Controller
    {
        //private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbMosque"].ConnectionString;
      
        public IActionResult Index()
        {
            //using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            //{
            //    connection.Open();
            //    // Bağlantıyı kullanarak veritabanı işlemlerini gerçekleştirin.
            //    connection.Close();
            //}


                return View();
        }
    }
}
