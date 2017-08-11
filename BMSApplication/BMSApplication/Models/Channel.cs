using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMSApplication.Models
{
    public class Channel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Label { get; set; }
        public bool Selected { get; set; }
        public DateTime Created { get; set; }
        public int CreateBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifyBy { get; set; }
    }
}