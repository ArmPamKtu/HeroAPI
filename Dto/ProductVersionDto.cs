using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class ProductVersionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public bool SoftDelete { get; set; }
        public string UrlImg { get; set; }
        public int ProductId { get; set; }
    }
}
