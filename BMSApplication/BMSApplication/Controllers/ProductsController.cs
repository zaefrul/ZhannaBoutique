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
    public class ProductsController : Controller
    {
        private BMSApplicationContext db = new BMSApplicationContext();

        // GET: Products
        public ActionResult Index()
        {
            ViewBag.Users = db.Users.ToList();
            //var products = db.Products.Where(p => p.Status != false);
            return View(db.Products.Where(p => p.Status != true).ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null || product.Status == true)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedName = db.Users.FirstOrDefault(u => u.Id == product.CreatedBy).Name;
            ViewBag.ModifiedName = db.Users.FirstOrDefault(u => u.Id == product.ModifyBy).Name;
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Category,Cost,Sell,Status,CreatedDate,CreatedBy,ModifyDate,ModifyBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedBy = 1;
                product.CreatedDate = DateTime.Now;
                product.ModifyBy = 1;
                product.ModifyDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null || product.Status == true)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Category,Cost,Sell,Status,CreatedDate,CreatedBy,ModifyDate,ModifyBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ModifyDate = DateTime.Now;
                product.ModifyBy = 1;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null || product.Status == true)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedName = db.Users.FirstOrDefault(u => u.Id == product.CreatedBy).Name;
            ViewBag.ModifiedName = db.Users.FirstOrDefault(u => u.Id == product.ModifyBy).Name;
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            product.Status = true;
            //db.Products.Remove(product);
            db.Entry(product).State = EntityState.Modified;
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
