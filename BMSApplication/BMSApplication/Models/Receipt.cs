using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMSApplication.Models
{
    public class Receipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string Method { get; set; }
        public double Tendered { get; set; }
        public double Change { get; set; }
        public string CreditCardInvoice { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}