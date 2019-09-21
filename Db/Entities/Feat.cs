using System;
using System.Collections.Generic;
using System.Text;

namespace Db.Entities
{
    public class Feat
    {
        public Guid FromGuid { get; set; }
        public Guid ToUserGuid { get; set; }
        public int Value { get; set; }
        public string Reason { get; set; }
        //public bool IsFromCommittee { get; set; }
        //public Guid CreatedBy { get; set; }
    }
}
