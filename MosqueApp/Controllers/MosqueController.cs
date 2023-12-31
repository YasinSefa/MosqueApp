﻿using Microsoft.AspNetCore.Http;
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
            try
            {
                var mos = _context.Mosques.Find(cityId);
                var ph = _context.Photos.Where(photo => photo.MosqueId == mos.Id).ToList();

                if (mosque.Town.Id == -1 || mosque.Town.Id == null || string.IsNullOrEmpty(mosque.Name) ||
                    string.IsNullOrEmpty(mosque.Title) || string.IsNullOrEmpty(mosque.Address) ||
                    string.IsNullOrEmpty(mosque.Coordinate) || string.IsNullOrEmpty(mosque.Description)
                    )
                {
                    var cities = _context.Cities.ToList();
                    var admins = _context.Admins.ToList();
                    var mosques = _context.Mosques.ToList();
                    var towns = _context.Towns.ToList();



                    if (QrCode != null && QrCode.Length > 0)

                    {
                        var memoryStream = new MemoryStream();
                        QrCode.CopyTo(memoryStream);
                        mosque.QrCode = memoryStream.ToArray();

                    }

                    var viewModel = new MosqueViewModel
                    {
                        Cities = cities,
                        Admins = admins,
                        Mosques = mosques,
                        Towns = towns,
                        Mosque = mos,
                        // Diğer özellikleri de burada doldurabilirsiniz.
                        // SelectedCityId = ...,
                        // SelectedTownId = ...,
                    };

                    TempData["ErrorMessage"] = " Lütfen tüm gereken alanları doldurun.";
                    return RedirectToAction("EditMosque", "Mosque", new { id = cityId });
                }



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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                return RedirectToAction("EditMosque", "Mosque", new { id = cityId });
            }
        }

        private object GetClientIpAddress(object current)
        {
            throw new NotImplementedException();
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
            try
            {

                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var timestamp = DateTime.UtcNow;
                var browser = Request.Headers["User-Agent"].ToString();
                var platform = Environment.OSVersion.Platform.ToString();


                if (mosque.TownId == -1 || mosque.TownId == null || string.IsNullOrEmpty(mosque.Name) || string.IsNullOrEmpty(mosque.Title) || string.IsNullOrEmpty(mosque.Address) || string.IsNullOrEmpty(mosque.Coordinate) || string.IsNullOrEmpty(mosque.Description) || QrCode == null || QrCode.Length == 0 || Photos1 == null || Photos1.Length == 0)
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

                        ViewData["ErrorMessage"] = " Lütfen tüm gereken alanları doldurun.";
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

                        ViewData["ErrorMessage"] = " Lütfen tüm gereken alanları doldurun.";
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
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;

                var cities = _context.Cities.ToList();
                var admins = _context.Admins.ToList();
                var towns = _context.Towns.ToList();

                var viewModel = new MosqueViewModel
                {
                    Cities = cities,
                    Admins = admins,
                    Towns = towns,
                    Mosque = mosque,
                };

                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult ListMosque()
        {
            try
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
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                return View("ErrorPage");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
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

                var action = "Deleted";
                var ph = _context.Photos.Where(photo => photo.MosqueId == mosque.Id).ToList();

                foreach (var photo in ph)
                {
                    var logPhotos = new LogPhotos
                    {
                        Id = photo.Id,
                        Base64 = photo.Base64,
                        MosqueId = photo.MosqueId,
                        AdminId = 1,
                        Action = action,
                        IpAddress = ipAddress,
                        Timestamp = timestamp,
                        Browser = browser,
                        Platform = platform,
                    };
                    _context.LogPhotos.Add(logPhotos);
                    _context.Photos.Remove(photo);
                }

                _context.LogMosques.Add(logMosque);
                _context.Mosques.Remove(mosque);
                _context.SaveChanges();

                return Json(logMosque);
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya hata mesajı döndürebilirsiniz.
                return BadRequest($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }
        }


        public IActionResult DeletePhoto(int id, int mosqueId)
        {
            try
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

                if (ph.Count > id)
                {
                    var photoToRemove = ph[id];

                    var logPhotos = new LogPhotos
                    {
                        Id = photoToRemove.Id,
                        Base64 = photoToRemove.Base64,
                        MosqueId = photoToRemove.MosqueId,
                        AdminId = 1,
                        Action = action,
                        IpAddress = ipAddress,
                        Timestamp = timestamp,
                        Browser = browser,
                        Platform = platform,
                    };

                    _context.LogPhotos.Add(logPhotos);
                    _context.Photos.Remove(photoToRemove);

                    _context.SaveChanges();
                }

                return Json(mosque);
            }
            catch (Exception ex)
            {
                return BadRequest($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }
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

        [HttpGet("Mosque/GetMosquesAsJson")]
        public ActionResult GetMosquesAsJson()
        {
            List<Mosque> mosques = _context.Mosques.ToList(); // Cami verilerini al

            // Cami listesini oluştururken her bir cami için Photos verilerini de ekleyin
            List<object> mosqueData = new List<object>();
            foreach (var mosque in mosques)
            {
                List<Photos> photosList = _context.Photos.Where(p => p.MosqueId == mosque.Id).ToList();
                mosque.Photos = photosList;

                mosqueData.Add(mosque);
            }

            string json = JsonConvert.SerializeObject(mosqueData); // JSON formatına çevir
            return Content(json, "application/json"); // JSON verilerini içeren bir ActionResult döndür
        }


        [HttpGet("Mosque/GetMosquesAsJson/{id}")]
        public ActionResult GetMosquesAsJson(int id)
        {
            Mosque mosque = _context.Mosques.FirstOrDefault(m => m.Id == id); // Id'ye göre camiyi bul

            if (mosque == null)
            {
                return NotFound(); // Eğer cami bulunamazsa 404 NotFound döndür
            }

            // Cami için ait resimleri de getir
            List<Photos> photosList = _context.Photos.Where(p => p.MosqueId == mosque.Id).ToList();
            mosque.Photos = photosList;

            string json = JsonConvert.SerializeObject(mosque); // Veriyi JSON formatına çevir
            return Content(json, "application/json"); // JSON verilerini içeren bir ActionResult döndür
        }


        [HttpGet("Mosque/GetByCitiesMosquesAsJson/{id}")]
        public ActionResult GetByCitiesMosquesAsJson(int id)
        {
            List<Mosque> mosques = _context.Mosques.Where(m => m.Town.CityId == id).ToList(); // Belirli şehirdeki camileri filtrele

            if (mosques.Count == 0)
            {
                return NotFound(); // Eğer cami bulunamazsa 404 NotFound döndür
            }

            // Cami listesini oluştururken her bir cami için Photos verilerini de ekleyin
            List<object> mosqueData = new List<object>();
            foreach (var mosque in mosques)
            {
                List<Photos> photosList = _context.Photos.Where(p => p.MosqueId == mosque.Id).ToList();
                mosque.Photos = photosList;

                mosqueData.Add(mosque);
            }

            string json = JsonConvert.SerializeObject(mosqueData); // Cami listesini JSON formatına çevir
            return Content(json, "application/json"); // JSON verilerini içeren bir ActionResult döndür
        }

        [HttpGet("Mosque/GetPhotosAsJson/{id}")]
        public ActionResult GetPhotosAsJson(int id)
        {
            List<Photos> photosList = _context.Photos.Where(p => p.MosqueId == id).ToList(); // Id'ye göre filtreleme
            if (photosList.Count == 0)
            {
                return NotFound(); // Eğer fotoğraf bulunamazsa 404 NotFound döndür
            }

            string json = JsonConvert.SerializeObject(photosList); // Veriyi JSON formatına çevir
            return Content(json, "application/json"); // JSON verilerini içeren bir ActionResult döndür
        }

        [HttpGet("Mosque/GetRandomMosquesAsJson/{count}")]
        public ActionResult GetRandomMosquesAsJson(int count)
        {
            if (count <= 0)
            {
                return BadRequest("Count must be a positive number.");
            }

            List<Mosque> allMosques = _context.Mosques.ToList();
            if (allMosques.Count <= count)
            {
                return BadRequest("O kadar cami yok");
            }

            Random random = new Random();
            List<Mosque> randomMosques = new List<Mosque>();

            while (randomMosques.Count < count)
            {
                int randomIndex = random.Next(0, allMosques.Count);
                if (!randomMosques.Contains(allMosques[randomIndex]))
                {
                    randomMosques.Add(allMosques[randomIndex]);
                }
            }

            // Cami listesini oluştururken her bir cami için Photos verilerini de ekleyin
            List<object> mosqueData = new List<object>();
            foreach (var mosque in randomMosques)
            {
                List<Photos> photosList = _context.Photos.Where(p => p.MosqueId == mosque.Id).ToList();
                mosque.Photos = photosList;

                mosqueData.Add(mosque);
            }

            string json = JsonConvert.SerializeObject(mosqueData);
            return Content(json, "application/json");
        }

        [HttpGet("Mosque/GetByTownsMosquesAsJson/{id}")]
        public ActionResult GetByTownsMosquesAsJson(int id)
        {
            List<Mosque> mosques = _context.Mosques.Where(m => m.Town.Id == id).ToList(); // Belirli şehirdeki camileri filtrele

            if (mosques.Count == 0)
            {
                return NotFound(); // Eğer cami bulunamazsa 404 NotFound döndür
            }

            // Cami listesini oluştururken her bir cami için Photos verilerini de ekleyin
            List<object> mosqueData = new List<object>();
            foreach (var mosque in mosques)
            {
                List<Photos> photosList = _context.Photos.Where(p => p.MosqueId == mosque.Id).ToList();
                mosque.Photos = photosList;

                mosqueData.Add(mosque);
            }

            string json = JsonConvert.SerializeObject(mosqueData); // Cami listesini JSON formatına çevir
            return Content(json, "application/json"); // JSON verilerini içeren bir ActionResult döndür
        }

        [HttpGet("Mosque/GetMosquesByNameAsJson/{name}")]
        public ActionResult GetMosquesByNameAsJson(string name)
        {
            // Cami adına göre arama yap
            List<Mosque> mosques = _context.Mosques.ToList(); // Tüm camileri çek

            mosques = mosques
                .Where(m => m.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) == 0 ||
                            m.Name.IndexOf(" " + name, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(m => m.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase))
                .ToList(); // Adı verilen camilere filtre uygula ve sırala

            if (mosques.Count == 0)
            {
                return NotFound(); // Eğer cami bulunamazsa 404 NotFound döndür
            }

            // Her bir cami için ait resimleri de getir
            foreach (var mosque in mosques)
            {
                List<Photos> photosList = _context.Photos.Where(p => p.MosqueId == mosque.Id).ToList();
                mosque.Photos = photosList;
            }

            string json = JsonConvert.SerializeObject(mosques); // Veriyi JSON formatına çevir
            return Content(json, "application/json"); // JSON verilerini içeren bir ActionResult döndür
        }
        public IActionResult GoogleMaps()
        {
            return View();
        }
    }

}
