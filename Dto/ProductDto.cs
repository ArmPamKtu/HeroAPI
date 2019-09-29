using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public int Quantity { get; set; }
        public bool IsInStore { get; set; }
        public bool IsOrderable { get; set; }
      
    }
}
