using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BMSApplication.Models;

namespace BMSApplication.ViewModels
{
    public class vInvoices
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<InvoiceItem> Items { get; set; }
    }
}