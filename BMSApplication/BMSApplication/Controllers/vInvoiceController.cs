using BMSApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMSApplication.Controllers
{
    public class vInvoiceController : Controller
    {
        private BMSApplicationContext db = new BMSApplicationContext();
        // GET: vInvoice
        public ActionResult Index()
        {
            return View();
        }

        // GET: vInvoice/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: vInvoice/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Brand");
            ViewBag.VarietyId = db.Varieties.Select(v => new SelectListItem { Value = v.Id.ToString(), Text = (v.Product.Name + " " + v.Size.Label + " " + v.Color.Label) }).ToList();
            return View();
        }

        // POST: vInvoice/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Brand");
            ViewBag.VarietyId = db.Varieties.Select(v => new SelectListItem { Value = v.Id.ToString(), Text = (v.Product.Name + " " + v.Size.Label + " " + v.Color.Label) }).ToList();

            try
            {
                // TODO: Add insert logic here
                ViewBag.Status = "Success";
                var inv = GetInvoice();
                if(inv != null)
                {
                    inv.Items = GetInvoiceItem();
                    db.Invoices.Add(inv);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
                return RedirectToRoute(new { controller = "Invoices", action = "Index" });
            }
            catch
            {
                return View();
            }
        }

        private Invoice GetInvoice()
        {
            Invoice i = new Invoice();
            try
            {
                i.Number = Request.Form["Number"];
                i.Date = DateTime.Parse(Request.Form["Date"]);
                i.GSTCode = Request.Form["GSTCode"];
                i.GSTAmount = Request.Form["GSTAmount"];
                i.SupplierId = int.Parse(Request.Form["SupplierId"]);
                i.VendorId = int.Parse(Request.Form["VendorId"]);
                i.Type = Request.Form["Type"];
            }
            catch
            {
                return null;
            }
            return i;
        }

        private List<InvoiceItem> GetInvoiceItem()
        {
            string[] varieties = Request.Form["VarietyId[]"].Split(',');
            string[] costs = Request.Form["cost[]"].Split(',');
            string[] sells = Request.Form["sell[]"].Split(',');
            string[] qtys = Request.Form["qty[]"].Split(',');
            string[] total = Request.Form["total[]"].Split(',');
            var invitems = new List<InvoiceItem>();
            for (int i = 0; i<varieties.Length; i++)
            {
                invitems.Add(new InvoiceItem {
                    Price = double.Parse(costs[i]),
                    Quantity = int.Parse(qtys[i]),
                    Total = double.Parse(total[i]),
                    VarietyId = int.Parse(varieties[i])
                });
            }
            return invitems.Count > 0 ? invitems : null;
        }

        // GET: vInvoice/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: vInvoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: vInvoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: vInvoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
