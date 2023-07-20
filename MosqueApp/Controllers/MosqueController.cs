using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MosqueApp.Controllers;
using MosqueApp.Models.DataContext;
using MosqueApp.Models.Model;
using MosqueApp.Models.ViewModel;
using NuGet.Protocol.Core.Types;

namespace MosqueApp.Controllers
{
    public class MosqueController : Controller
    {
        private MosqueContext _context;

        public MosqueController()
        {
           this._context = new MosqueContext();
        }

        // GET: MosqueController
        public ActionResult Index()
        {

            return View();
        }

        // GET: MosqueController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MosqueController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MosqueController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MosqueController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MosqueController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MosqueController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MosqueController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public JsonResult ShowTowns(int Id)
        {
            var state = _context.Towns.Where(x => x.CityId == Id);
            return Json(new SelectList(state,"Id","Name"));
        }

        public IActionResult NewMosque()
        {
            var cities = _context.Cities.ToList();
            var admins = _context.Admins.ToList();
            var mosques = _context.Mosques.ToList();
            var towns = _context.Towns.ToList();

            var viewModel = new MosqueViewModel
            {
                Cities = cities,
                Admins = admins,
                Mosques = mosques,
                Towns = towns,
                // Diğer özellikleri de burada doldurabilirsiniz.
                // SelectedCityId = ...,
                // SelectedTownId = ...,
            };

            return View(viewModel);
        }

        public IActionResult ListMosque()
        {
            var degerler = _context.Mosques.ToList();
            return View(degerler);
        }

        [HttpGet]
        public IActionResult SelectedCityId()
        {
            // selectedCityId, select list'ten seçilen şehir ID'sini içerecektir.
            // İşlemlerinizi burada gerçekleştirin.
            // Örneğin, veriyi veritabanına kaydedebilirsiniz.

            return RedirectToAction("Success"); // Başarılı sayfasına yönlendirme yapabilirsiniz.
        }

        [HttpPost]
        public IActionResult SelectedCityId(MosqueViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Şehir nesnesini ViewModel'den doğrudan alabiliriz.
                City selectedCity = viewModel.Cities.FirstOrDefault(city => city.Id == viewModel.SelectedCityId);

                if (selectedCity != null)
                {
                    // Şimdi seçilen şehir nesnesini kullanabiliriz.
                    // selectedCity.Id veya selectedCity.Name gibi özelliklere erişebiliriz.
                    int selectedCityId = selectedCity.Id;

                    // Diğer işlemleri yapabilir ve veritabanına kaydedebiliriz.
                    // ...

                    return RedirectToAction("Index", "Home"); // Örnek bir yönlendirme.
                }
            }

            // Eğer ModelState geçerli değilse veya şehir seçilmemişse, aynı sayfada kalınabilir veya uygun bir hata mesajı gösterilebilir.
            return View("NewMosque", viewModel);
        }


    }
}
