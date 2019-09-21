using System;
using System.Collections.Generic;
using System.Text;

namespace Db.Entities
{
    public class Product
    {
        public int Quantity { get; set; }
        public bool IsInStore { get; set; }
        public bool IsOrderable { get; set; }
        //public ICollection<ProductVersion> ProductVersion { get; set; }
    }
}
