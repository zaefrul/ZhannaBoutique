using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMSApplication.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string GSTCode { get; set; }
        public string GSTAmount { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public string Type { get; set; }
        public ICollection<InvoiceItem> Items { get; set; }
    }
}