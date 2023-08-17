using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MosqueApp.Controllers;
using MosqueApp.Models.DataContext;
using MosqueApp.Models.Model;
using MosqueApp.Models.ViewModel;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System;
using System.Data.Entity;
using System.IO;
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

            //ViewBag.MosqueId = mosques.Where(x=> x.Id == mosque.Id).ToList().FirstOrDefault().Town.Id;

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

            return View("EditMosque", viewModel);
        }




        public IActionResult UpdateMosque(Mosque mosque, int cityId, IFormFile Photos1, IFormFile Photos2, IFormFile Photos3, IFormFile QrCode, Photos photos)
        {
            var mos = _context.Mosques.Find(cityId);

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var timestamp = DateTime.UtcNow;
            var browser = Request.Headers["User-Agent"].ToString();
            var platform = Environment.OSVersion.Platform.ToString();
            var action = "Updated";
            var adminId = 1;

            var logMosque = new LogMosque
            {
                Id = mos.Id,
                Name = mos.Name,
                Address = mos.Address,
                TownId = mos.TownId,
                Coordinate = mos.Coordinate,
                Description = mos.Description,
                Title = mos.Title,
                QrCode = mos.QrCode,
                AdminId = adminId,
                Action = action,
                IpAddress = ipAddress,
                Timestamp = timestamp,
                Browser = browser,
                Platform = platform,
            };

            _context.LogMosques.Add(logMosque);
            _context.SaveChanges();

            mos.TownId = mosque.Town.Id;
            mos.Name = mosque.Name;
            mos.Address = mosque.Address;
            mos.Coordinate = mosque.Coordinate;
            mos.Description = mosque.Description;

            var ph = _context.Photos.Where(photo => photo.MosqueId == mos.Id).ToList();

            if (Photos1 != null && Photos1.Length > 0)
            {
                // Fotoğrafları base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {
                    if (ph.Count != 0)
                    {
                        var logPhotos = new LogPhotos
                        {
                            Id = ph[0].Id,
                            Base64 = ph[0].Base64,
                            MosqueId = ph[0].MosqueId,
                            AdminId = adminId,
                            Action = action,
                            IpAddress = ipAddress,
                            Timestamp = timestamp,
                            Browser = browser,
                            Platform = platform,
                        };

                        _context.LogPhotos.Add(logPhotos);

                        Photos1.CopyTo(memoryStream);
                        ph[0].Base64 = memoryStream.ToArray();
                    }
                    else
                    {
                        Photos pho = new Photos();
                        pho.MosqueId = mos.Id;
                        Photos1.CopyTo(memoryStream);
                        pho.Base64 = memoryStream.ToArray();
                        pho.AdminId = adminId;
                        pho.IpAddress = ipAddress;
                        pho.Timestamp = timestamp;
                        pho.Platform = platform;
                        pho.Action = action;
                        pho.Browser = browser;

                        _context.Photos.Add(pho);

                    }

                    _context.SaveChanges();
                }
            }

            if (Photos2 != null && Photos2.Length > 0)
            {
                // Fotoğrafları base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {
                    if (ph.Count > 1)
                    {
                        var logPhotos = new LogPhotos
                        {
                            Id = ph[1].Id,
                            Base64 = ph[1].Base64,
                            MosqueId = ph[1].MosqueId,
                            AdminId = adminId,
                            Action = action,
                            IpAddress = ipAddress,
                            Timestamp = timestamp,
                            Browser = browser,
                            Platform = platform,
                        };

                        _context.LogPhotos.Add(logPhotos);
                        Photos2.CopyTo(memoryStream);
                        ph[1].Base64 = memoryStream.ToArray();
                    }
                    else
                    {
                        Photos pho = new Photos();
                        pho.MosqueId = mos.Id;
                        Photos2.CopyTo(memoryStream);
                        pho.Base64 = memoryStream.ToArray();
                        pho.AdminId = adminId;
                        pho.IpAddress = ipAddress;
                        pho.Timestamp = timestamp;
                        pho.Platform = platform;
                        pho.Action = action;
                        pho.Browser = browser;

                        _context.Photos.Add(pho);
                    }
                    _context.SaveChanges();
                }
            }

            if (Photos3 != null && Photos3.Length > 0)
            {
                // Fotoğrafları base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {
                    if (ph.Count > 2)
                    {
                        var logPhotos = new LogPhotos
                        {
                            Id = ph[2].Id,
                            Base64 = ph[2].Base64,
                            MosqueId = ph[2].MosqueId,
                            AdminId = adminId,
                            Action = action,
                            IpAddress = ipAddress,
                            Timestamp = timestamp,
                            Browser = browser,
                            Platform = platform,
                        };

                        _context.LogPhotos.Add(logPhotos);
                        Photos3.CopyTo(memoryStream);
                        ph[2].Base64 = memoryStream.ToArray();
                    }
                    else
                    {
                        Photos pho = new Photos();
                        pho.MosqueId = mos.Id;
                        Photos3.CopyTo(memoryStream);
                        pho.Base64 = memoryStream.ToArray();
                        pho.AdminId = adminId;
                        pho.IpAddress = ipAddress;
                        pho.Timestamp = timestamp;
                        pho.Platform = platform;
                        pho.Action = action;
                        pho.Browser = browser;

                        _context.Photos.Add(pho);
                    }


                    _context.SaveChanges();
                }
            }

            if (QrCode != null && QrCode.Length > 0)
            {
                // QR Kodu resmini base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {
                    QrCode.CopyTo(memoryStream);
                    mos.QrCode = memoryStream.ToArray();
                }
            }




            _context.SaveChanges();
            return RedirectToAction("ListMosque");
        }

        private object GetClientIpAddress(object current)
        {
            throw new NotImplementedException();
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
            return Json(new SelectList(state, "Id", "Name"));
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
        public IActionResult NewMosque(Mosque mosque, IFormFile QrCode, IFormFile Photos1, IFormFile Photos2, IFormFile Photos3, Photos photos)
        {

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var timestamp = DateTime.UtcNow;
            var browser = Request.Headers["User-Agent"].ToString();
            var platform = Environment.OSVersion.Platform.ToString();


            if (mosque.TownId == null || string.IsNullOrEmpty(mosque.Name) || string.IsNullOrEmpty(mosque.Title) || string.IsNullOrEmpty(mosque.Address) || string.IsNullOrEmpty(mosque.Coordinate) || string.IsNullOrEmpty(mosque.Description) || QrCode == null || QrCode.Length == 0 || Photos1 == null || Photos1.Length == 0)
            {
                var cities = _context.Cities.ToList();
                var admins = _context.Admins.ToList();
                var towns = _context.Towns.ToList();



                if (QrCode != null && QrCode.Length > 0)

                {
                    var memoryStream = new MemoryStream();
                    QrCode.CopyTo(memoryStream);
                    mosque.QrCode = memoryStream.ToArray();
                    var viewModel = new MosqueViewModel
                    {
                        Cities = cities,
                        Admins = admins,
                        Towns = towns,
                        Mosque = mosque,
                        // Diğer özellikleri de burada doldurabilirsiniz.
                        // SelectedCityId = ...,
                        // SelectedTownId = ...,
                    };
                    return View(viewModel);

                }
                else
                {
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

            }


            mosque.IpAddress = ipAddress;
            mosque.Timestamp = timestamp;
            mosque.Platform = platform;
            mosque.Browser = browser;
            mosque.AdminId = 1;
            mosque.Action = "Added";

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

            _context.Mosques.Add(mosque);
            _context.SaveChanges();

            var sonveri = _context.Mosques.OrderByDescending(item => item.Id).FirstOrDefault();

            if (Photos1 != null && Photos1.Length > 0)
            {
                // Fotoğrafları base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {

                    photos.MosqueId = sonveri.Id;
                    Photos1.CopyTo(memoryStream);
                    photos.Base64 = memoryStream.ToArray();
                    photos.Action = mosque.Action;
                    photos.AdminId = mosque.AdminId;
                    photos.Browser = browser;
                    photos.Platform = platform;
                    photos.Timestamp = timestamp;
                    photos.IpAddress = ipAddress;

                    _context.Photos.Add(photos);
                    _context.SaveChanges();
                }
            }


            if (Photos2 != null && Photos2.Length > 0)
            {
                // Fotoğrafları base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {
                    Photos ph = new Photos();
                    ph.MosqueId = sonveri.Id;
                    Photos2.CopyTo(memoryStream);
                    ph.Base64 = memoryStream.ToArray();
                    ph.Action = mosque.Action;
                    ph.AdminId = mosque.AdminId;
                    ph.Browser = browser;
                    ph.Platform = platform;
                    ph.Timestamp = timestamp;
                    ph.IpAddress = ipAddress;

                    _context.Photos.Add(ph);
                    _context.SaveChanges();
                }
            }
            // Veritabanına ekleme işlemi


            if (Photos3 != null && Photos3.Length > 0)
            {
                // Fotoğrafları base64 formatında dönüştürün
                using (var memoryStream = new MemoryStream())
                {
                    Photos ph = new Photos();
                    ph.MosqueId = sonveri.Id;
                    Photos3.CopyTo(memoryStream);
                    ph.Base64 = memoryStream.ToArray();
                    ph.Action = mosque.Action;
                    ph.AdminId = mosque.AdminId;
                    ph.Browser = browser;
                    ph.Platform = platform;
                    ph.Timestamp = timestamp;
                    ph.IpAddress = ipAddress;


                    _context.Photos.Add(ph);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("ListMosque");
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

        [HttpGet]
        public IActionResult Delete(int id)
        {

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var timestamp = DateTime.UtcNow;
            var browser = Request.Headers["User-Agent"].ToString();
            var platform = Environment.OSVersion.Platform.ToString();

            if (id == null)
            {
                return NotFound();
            }

            var mosque = _context.Mosques.FirstOrDefault(m => m.Id == id);

            if (mosque == null)
            {
                return NotFound();
            }


            // Seçilen mosque verisini "DeletedMosque" tablosuna ekleyelim.
            var logMosque = new LogMosque
            {
                Id = mosque.Id,
                Name = mosque.Name,
                Address = mosque.Address,
                TownId = mosque.TownId,
                Coordinate = mosque.Coordinate,
                Description = mosque.Description,
                Title = mosque.Title,
                QrCode = mosque.QrCode,
                AdminId = 1,
                Action = "Deleted",
                IpAddress = ipAddress,
                Timestamp = timestamp,
                Browser = browser,
                Platform = platform,
            };

            _context.LogMosques.Add(logMosque);

            var action = "Deleted";
            var ph = _context.Photos.Where(photo => photo.MosqueId == mosque.Id).ToList();
            // Seçilen mosque verisini "DeletedMosque" tablosuna ekleyelim.
            if (ph.Count != 0)
            {
                var logPhotos = new LogPhotos
                {
                    Id = ph[0].Id,
                    Base64 = ph[0].Base64,
                    MosqueId = ph[0].MosqueId,
                    AdminId = 1,
                    Action = action,
                    IpAddress = ipAddress,
                    Timestamp = timestamp,
                    Browser = browser,
                    Platform = platform,
                };
                _context.LogPhotos.Add(logPhotos);
                _context.Photos.Remove(ph[0]);
            }
            if (ph.Count > 1)
            {
                var logPhotos = new LogPhotos
                {
                    Id = ph[1].Id,
                    Base64 = ph[1].Base64,
                    MosqueId = ph[1].MosqueId,
                    AdminId = 1,
                    Action = action,
                    IpAddress = ipAddress,
                    Timestamp = timestamp,
                    Browser = browser,
                    Platform = platform,
                };
                _context.LogPhotos.Add(logPhotos);
                _context.Photos.Remove(ph[1]);
            }
            if (ph.Count > 2)
            {
                var logPhotos = new LogPhotos
                {
                    Id = ph[2].Id,
                    Base64 = ph[2].Base64,
                    MosqueId = ph[2].MosqueId,
                    AdminId = 1,
                    Action = action,
                    IpAddress = ipAddress,
                    Timestamp = timestamp,
                    Browser = browser,
                    Platform = platform,
                };
                _context.LogPhotos.Add(logPhotos);
                _context.Photos.Remove(ph[2]);
            }



            try
            {
                _context.Mosques.Remove(mosque);
                _context.SaveChanges();

                // Veriyi orijinal tablodan silelim

                //_context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya hata mesajı döndürebilirsiniz.
                return BadRequest($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }


            return Json(logMosque);
        }

        public IActionResult DeletePhoto(int id, int mosqueId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var timestamp = DateTime.UtcNow;
            var browser = Request.Headers["User-Agent"].ToString();
            var platform = Environment.OSVersion.Platform.ToString();
            var action = "Deleted";

            var mosque = _context.Mosques.FirstOrDefault(m => m.Id == mosqueId);
            var ph = _context.Photos.Where(photo => photo.MosqueId == mosqueId).ToList();
            // Seçilen mosque verisini "DeletedMosque" tablosuna ekleyelim.
            if (ph.Count != 0)
            {
                if (id == 0 && ph.Count != 0)
                {
                    var logPhotos = new LogPhotos
                    {
                        Id = ph[0].Id,
                        Base64 = ph[0].Base64,
                        MosqueId = ph[0].MosqueId,
                        AdminId = 1,
                        Action = action,
                        IpAddress = ipAddress,
                        Timestamp = timestamp,
                        Browser = browser,
                        Platform = platform,
                    };
                    _context.LogPhotos.Add(logPhotos);
                    _context.Photos.Remove(ph[0]);
                }
                else if (id == 1 && ph.Count > 1)
                {
                    var logPhotos = new LogPhotos
                    {
                        Id = ph[1].Id,
                        Base64 = ph[1].Base64,
                        MosqueId = ph[1].MosqueId,
                        AdminId = 1,
                        Action = action,
                        IpAddress = ipAddress,
                        Timestamp = timestamp,
                        Browser = browser,
                        Platform = platform,
                    };
                    _context.LogPhotos.Add(logPhotos);
                    _context.Photos.Remove(ph[1]);
                }
                else if (id == 2 && ph.Count > 2)
                {
                    var logPhotos = new LogPhotos
                    {
                        Id = ph[2].Id,
                        Base64 = ph[2].Base64,
                        MosqueId = ph[2].MosqueId,
                        AdminId = 1,
                        Action = action,
                        IpAddress = ipAddress,
                        Timestamp = timestamp,
                        Browser = browser,
                        Platform = platform,
                    };
                    _context.LogPhotos.Add(logPhotos);
                    _context.Photos.Remove(ph[2]);
                }
            }




            try
            {
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya hata mesajı döndürebilirsiniz.
                return BadRequest($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }


            return Json(mosque);
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
        public IActionResult GetPhotos(int id)
        {
            var mosque = _context.Mosques.Include(m => m.Photos).FirstOrDefault(m => m.Id == id);
            if (mosque == null)
            {
                return NotFound();
            }

            var photoResults = new List<object>();

            foreach (var photo in mosque.Photos)
            {
                byte[] photoData = photo.Base64;
                var base64Photo = Convert.ToBase64String(photoData);
                photoResults.Add(new { photo = base64Photo });
            }

            return Json(photoResults);
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

            var photos = _context.Photos.Where(p => p.MosqueId == id).ToList();

            var viewModel = new MosqueViewModel
            {
                Cities = cities,
                Admins = admins,
                Mosques = mosques,
                Towns = towns,
                Mosque = mosque,
                Photos = photos,

            };

            return Json(viewModel);
        }

        [HttpGet]
        public IActionResult GetPhotosByMosqueId(int mosqueId)
        {
            var photos = _context.Photos.Where(p => p.MosqueId == mosqueId).ToList();
            var photoResults = photos.Select(photo => new { photo = Convert.ToBase64String(photo.Base64) });
            return Json(photoResults);

        }

        public ActionResult GetProductsAsJson()
        {
            List<Mosque> products = _context.Mosques.ToList(); // Verileri al
            string json = JsonConvert.SerializeObject(products); // Verileri JSON formatına çevir
            return Content(json, "application/json"); // JSON verilerini içeren bir ActionResult döndür
        }

    }

}
