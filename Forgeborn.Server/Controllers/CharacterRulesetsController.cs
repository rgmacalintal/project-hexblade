using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    public class CharacterRulesetsController : Controller
    {
        // GET: CharacterRulesetsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CharacterRulesetsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CharacterRulesetsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CharacterRulesetsController/Create
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

        // GET: CharacterRulesetsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CharacterRulesetsController/Edit/5
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

        // GET: CharacterRulesetsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CharacterRulesetsController/Delete/5
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
