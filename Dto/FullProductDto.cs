using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class FullProductDto
    {
        public Guid ProductId { get; set; }
        public DateTime ProductCreated { get; set; }
        public int Quantity { get; set; }
        public bool IsInStore { get; set; }
        public bool IsOrderable { get; set; }


        public Guid ProductVersionId { get; set; }
        public string Name { get; set; }
        public DateTime ProductVersionCreated { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public bool SoftDelete { get; set; }
        public string UrlImg { get; set; }
    }
}
