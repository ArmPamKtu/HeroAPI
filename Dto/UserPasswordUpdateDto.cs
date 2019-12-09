using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dto
{
    public class UserPasswordUpdateDto
    {
        public string Id { get; set; }
        [Required]
        public string OldPassword { get; set; } //specify password requirements
        [Required]
        public string NewPassword { get; set; } //specify password requirements
    }
}
