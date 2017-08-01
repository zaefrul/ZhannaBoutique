using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BMSApplication.Models;

namespace BMSApplication.Controllers
{
    public class VarietiesController : Controller
    {
        private BMSApplicationContext db = new BMSApplicationContext();

        // GET: Varieties
        public ActionResult Index()
        {
            var varieties = db.Varieties.Include(v => v.Color).Include(v => v.Product).Include(v => v.Size);
            return View(varieties.ToList());
        }

        // GET: Varieties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variety variety = db.Varieties.Find(id);
            if (variety == null)
            {
                return HttpNotFound();
            }
            return View(variety);
        }

        // GET: Varieties/Create
        public ActionResult Create()
        {
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Label");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "Label");
            return View();
        }

        // POST: Varieties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,SizeId,ColorId,Quantity")] Variety variety)
        {
            if (ModelState.IsValid)
            {
                db.Varieties.Add(variety);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Label", variety.ColorId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", variety.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "Label", variety.SizeId);
            return View(variety);
        }

        // GET: Varieties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variety variety = db.Varieties.Find(id);
            if (variety == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Label", variety.ColorId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", variety.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "Label", variety.SizeId);
            return View(variety);
        }

        // POST: Varieties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,SizeId,ColorId,Quantity")] Variety variety)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variety).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorId = new SelectList(db.Colors, "Id", "Label", variety.ColorId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", variety.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "Label", variety.SizeId);
            return View(variety);
        }

        // GET: Varieties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variety variety = db.Varieties.Find(id);
            if (variety == null)
            {
                return HttpNotFound();
            }
            return View(variety);
        }

        // POST: Varieties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Variety variety = db.Varieties.Find(id);
            db.Varieties.Remove(variety);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
