using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.WebUI.Controllers
{
    public class FishDefinitionController : Controller
    {
        // GET: FishDefinition
        public ActionResult Index()
        {
            return View();
        }

        // GET: FishDefinition/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FishDefinition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FishDefinition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FishDefinition/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FishDefinition/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FishDefinition/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FishDefinition/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}