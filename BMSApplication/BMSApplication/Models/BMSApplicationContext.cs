using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BMSApplication.Models
{
    public class BMSApplicationContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BMSApplicationContext() : base("name=BMSApplicationContext")
        {
        }

        public System.Data.Entity.DbSet<BMSApplication.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<BMSApplication.Models.Color> Colors { get; set; }

        public System.Data.Entity.DbSet<BMSApplication.Models.Size> Sizes { get; set; }

        public System.Data.Entity.DbSet<BMSApplication.Models.Variety> Varieties { get; set; }

        public System.Data.Entity.DbSet<BMSApplication.Models.Receipt> Receipts { get; set; }

        public System.Data.Entity.DbSet<BMSApplication.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<BMSApplication.Models.Transaction> Transactions { get; set; }
    }
}
