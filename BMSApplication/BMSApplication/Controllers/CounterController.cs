using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BMSApplication.Models;
using BMSApplication.ViewModels;
using System.Net;

namespace BMSApplication.Controllers
{
    public class CounterController : Controller
    {
        private BMSApplicationContext db = new BMSApplicationContext();
        // GET: Counter
        public ActionResult Index()
        {
            
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name");
            ViewBag.ChannelId = new SelectList(db.Channels, "Id", "Label");
            object[] payment = new object[2];
            payment[0] = "Cash";
            payment[1] = "Credit Card";
            ViewBag.PaymentMethod = new SelectList(payment);
            var varieties = db.Varieties.Include(v => v.Color).Include(v => v.Product).Include(v => v.Size);
           
           return View(varieties.ToList());
            
        }
        //[Route("customer/detail/{id}")]
        public ActionResult Details(int? id)
        {
            /*ViewBag.Tendered = 3;
            return View();*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.Transaction = db.Transactions.Where(t => t.ReceiptId == id);
            //Console.WriteLine(ViewBag.Transaction);
            return View(receipt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int[] quantity, double[] cost, int[] id, double[] sell, double tendered, double totalCost, int MemberId, string PaymentMethod, double change, int ChannelId, string credit_invoice)
        {
            //var variety = db.Varieties.FirstOrDefault(u => u.Id == 1);
            Receipt receipt = new Receipt();
            receipt.MemberId = MemberId;
            receipt.Method = PaymentMethod;
            receipt.Tendered = tendered;
            receipt.Total = totalCost;
            receipt.Change = change;
            receipt.CreditCardInvoice = credit_invoice;
            receipt.Date = DateTime.Now;
            db.Receipts.Add(receipt);
            db.SaveChanges();
            int receiptId = receipt.Id;

            int count = 0;
            foreach(var i in id)
            {
                var variety = db.Varieties.FirstOrDefault(u => u.Id == i);
                variety.Quantity = variety.Quantity - quantity[count];
                db.Entry(variety).State = EntityState.Modified;
                db.SaveChanges();
                Transaction transaction = new Transaction();
                transaction.VarietyId = i;
                transaction.Date = DateTime.Now;
                transaction.ReceiptId = receiptId;
                transaction.UserId = 1;
                transaction.Sell = sell[count];
                transaction.ChannelId = ChannelId;
                transaction.Quantity = quantity[count];
                db.Transactions.Add(transaction);
                db.SaveChanges();
                //variety.Quantity -
                //Console.WriteLine(i);
                count++;
            }



            //return Content(receiptId.ToString());
            return RedirectToAction("Details/" + receiptId);
        }
        

    }
}