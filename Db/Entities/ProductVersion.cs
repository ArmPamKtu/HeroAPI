using System;
using System.Collections.Generic;
using System.Text;

namespace Db.Entities
{
    public class ProductVersion
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public bool SoftDelete { get; set; }
        public string UrlImg { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
