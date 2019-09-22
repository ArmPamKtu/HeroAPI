﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Db.Entities
{
    class UserRoles
    {
        [Key]
        public Guid UserGuid { get; set; }
        public DateTime Created { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsStoreManager { get; set; }
    }
}