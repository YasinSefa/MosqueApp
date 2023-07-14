using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MosqueApp.Controllers;
using MosqueApp.Models.DataContext;

namespace MosqueApp.Controllers
{
    public class MosqueController : Controller
    {

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
    }
}
