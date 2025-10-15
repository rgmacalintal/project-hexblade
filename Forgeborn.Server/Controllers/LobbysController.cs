using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    public class LobbysController : Controller
    {
        // GET: LobbysController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LobbysController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LobbysController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LobbysController/Create
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

        // GET: LobbysController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LobbysController/Edit/5
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

        // GET: LobbysController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LobbysController/Delete/5
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
