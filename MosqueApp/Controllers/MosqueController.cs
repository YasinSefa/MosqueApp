using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MosqueApp.Controllers;
using MosqueApp.Models.DataContext;
using MosqueApp.Models.Model;
using MosqueApp.Models.ViewModel;
using NuGet.Protocol.Core.Types;
using System.Text.Json.Nodes;

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
        public IActionResult EditMosque(int id)
        {
            var cities = _context.Cities.ToList();
            var admins = _context.Admins.ToList();
            var mosques = _context.Mosques.ToList();
            var towns = _context.Towns.ToList();
            var mosque = _context.Mosques.Find(id);

            ViewBag.MosqueId = mosques.Where(x=> x.Id == mosque.Id).ToList().FirstOrDefault().Town.Id;

            var viewModel = new MosqueViewModel
            {
                Cities = cities,
                Admins = admins,
                Mosques = mosques,
                Towns = towns,
                Mosque = mosque,
                // Diğer özellikleri de burada doldurabilirsiniz.
                // SelectedCityId = ...,
                // SelectedTownId = ...,
            };

            return View("EditMosque",viewModel);
        }






        public IActionResult UpdateMosque(Mosque mosque,int cityId)
        {
            var mos = _context.Mosques.Find(cityId);
            mos.TownId = mosque.Town.Id;
            mos.Name = mosque.Name;
            mos.Address = mosque.Address;
            mos.Coordinate = mosque.Coordinate;
            mos.Description = mosque.Description;
            _context.SaveChanges();
            return RedirectToAction("ListMosque");
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

        [HttpGet]
        public IActionResult NewMosque()
        {
            var cities = _context.Cities.ToList();
            var admins = _context.Admins.ToList();
            var towns = _context.Towns.ToList();

            var viewModel = new MosqueViewModel
            {
                Cities = cities,
                Admins = admins,
                Towns = towns,
                // Diğer özellikleri de burada doldurabilirsiniz.
                // SelectedCityId = ...,
                // SelectedTownId = ...,
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NewMosque(Mosque mosque, IFormFile QrCode, IFormFile Photos)
        {
            if (mosque.TownId == null || string.IsNullOrEmpty(mosque.Name) || string.IsNullOrEmpty(mosque.Title) || string.IsNullOrEmpty(mosque.Address) || string.IsNullOrEmpty(mosque.Coordinate) || string.IsNullOrEmpty(mosque.Description) || QrCode == null || QrCode.Length == 0 || Photos == null || Photos.Length == 0)
            {
                ModelState.AddModelError("", "Tüm alanları doldurmalısınız.");

                var cities = _context.Cities.ToList();
                var admins = _context.Admins.ToList();
                var towns = _context.Towns.ToList();

                var viewModel = new MosqueViewModel
                {
                    Cities = cities,
                    Admins = admins,
                    Towns = towns,
                    // Diğer özellikleri de burada doldurabilirsiniz.
                    // SelectedCityId = ...,
                    // SelectedTownId = ...,
                };

                return View(viewModel);
            }

            // Veri doğrulama başarılı, devam edin

            if (QrCode != null && QrCode.Length > 0)
            {
                // QR Kodu resmini base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {
                    QrCode.CopyTo(memoryStream);
                    mosque.QrCode = memoryStream.ToArray();
                }
            }

            if (Photos != null && Photos.Length > 0)
            {
                // Fotoğrafları base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {
                    Photos.CopyTo(memoryStream);
                    mosque.Photos = memoryStream.ToArray();
                }
            }

            // Veritabanına ekleme işlemi
            _context.Mosques.Add(mosque);
            _context.SaveChanges();
            return RedirectToAction("NewMosque");
        }


        public IActionResult ListMosque()
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


        public IActionResult Delete(int id)
        {
            var mos = _context.Mosques.Find(id);
            _context.Mosques.Remove(mos);
            _context.SaveChanges();
            return RedirectToAction("ListMosque");
        }

        [HttpGet]
        public IActionResult SelectedCityId()
        {
            // selectedCityId, select list'ten seçilen şehir ID'sini içerecektir.
            // İşlemlerinizi burada gerçekleştirin.
            // Örneğin, veriyi veritabanına kaydedebilirsiniz.

            return RedirectToAction("Success"); // Başarılı sayfasına yönlendirme yapabilirsiniz.
        }

        [HttpGet]
        public IActionResult GetPhoto(int id)
        {
            var mosque = _context.Mosques.FirstOrDefault(m => m.Id == id);
            if (mosque == null)
            {
                return NotFound(); // 404 Not Found dönebiliriz
            }

            // Fotoğrafı byte dizisi olarak alıyoruz.
            byte[] photoData = mosque.Photos;

            // byte dizisini base64 formatına çevirerek JSON olarak dönüyoruz.
            var base64Photo = Convert.ToBase64String(photoData);
            return Json(new { photo = base64Photo });
        }

        [HttpGet]
        public IActionResult GetQrCode(int id)
        {
            var mosque = _context.Mosques.FirstOrDefault(m => m.Id == id);
            if (mosque == null)
            {
                return NotFound(); // 404 Not Found dönebiliriz
            }

            // QR kodunu byte dizisi olarak alıyoruz.
            byte[] qrCodeData = mosque.QrCode;

            // byte dizisini base64 formatına çevirerek JSON olarak dönüyoruz.
            var base64QrCode = Convert.ToBase64String(qrCodeData);
            return Json(new { qrCode = base64QrCode });
        }


        // GET: MosqueController/Edit/5
        public IActionResult DetailsMosque(int id)
        {
            var cities = _context.Cities.ToList();
            var admins = _context.Admins.ToList();
            var mosques = _context.Mosques.ToList();
            var towns = _context.Towns.ToList();
            var mosque = _context.Mosques.Find(id);



            var viewModel = new MosqueViewModel
            {
                Cities = cities,
                Admins = admins,
                Mosques = mosques,
                Towns = towns,
                Mosque = mosque,

            };

            return Json(viewModel);
        }

    }
}
