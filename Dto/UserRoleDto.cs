using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class UserRoleDto
    {
        public Guid UserGuid { get; set; }
        public DateTime Created { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsStoreManager { get; set; }
    }
}
