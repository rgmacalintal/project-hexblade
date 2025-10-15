using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    public class RulesetsController : Controller
    {
        // GET: RulesetsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RulesetsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RulesetsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RulesetsController/Create
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

        // GET: RulesetsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RulesetsController/Edit/5
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

        // GET: RulesetsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RulesetsController/Delete/5
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
