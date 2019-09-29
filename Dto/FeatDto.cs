using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class FeatDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid FromGuid { get; set; }
        public Guid ToUserGuid { get; set; }
        public int Value { get; set; }
        public string Reason { get; set; }
    }
}
