using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketeers.Controllers
{
    public class DisplayOrderController : Controller
    {
        // GET: DisplayOrder
        public ActionResult Index()
        {
            return View();
        }

        // GET: DisplayOrder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DisplayOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DisplayOrder/Create
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

        // GET: DisplayOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DisplayOrder/Edit/5
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

        // GET: DisplayOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DisplayOrder/Delete/5
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
