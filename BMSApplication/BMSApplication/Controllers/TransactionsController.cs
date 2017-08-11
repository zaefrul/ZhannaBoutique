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
    public class TransactionsController : Controller
    {
        private BMSApplicationContext db = new BMSApplicationContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Channel).Include(t => t.Receipt).Include(t => t.User).Include(t => t.Variety);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Label");
            ViewBag.ReceiptId = new SelectList(db.Receipts, "Id", "Method");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.VarietyId = new SelectList(db.Varieties, "Id", "Id");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,VarietyId,ReceiptId,UserId,Sell,ChannelId,Quantity")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Label", transaction.ChannelId);
            ViewBag.ReceiptId = new SelectList(db.Receipts, "Id", "Method", transaction.ReceiptId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", transaction.UserId);
            ViewBag.VarietyId = new SelectList(db.Varieties, "Id", "Id", transaction.VarietyId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Label", transaction.ChannelId);
            ViewBag.ReceiptId = new SelectList(db.Receipts, "Id", "Method", transaction.ReceiptId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", transaction.UserId);
            ViewBag.VarietyId = new SelectList(db.Varieties, "Id", "Id", transaction.VarietyId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,VarietyId,ReceiptId,UserId,Sell,ChannelId,Quantity")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Label", transaction.ChannelId);
            ViewBag.ReceiptId = new SelectList(db.Receipts, "Id", "Method", transaction.ReceiptId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", transaction.UserId);
            ViewBag.VarietyId = new SelectList(db.Varieties, "Id", "Id", transaction.VarietyId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
