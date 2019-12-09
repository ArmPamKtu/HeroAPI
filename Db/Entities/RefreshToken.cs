using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Db.Entities
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        [Required]
        public string JwtId { get; set; }
        [Required]
        public DateTime? CreationDate { get; set; }
        [Required]
        public DateTime? ExpirationDate { get; set; }
        public bool Used { get; set; } = false;
        public bool Invalidated { get; set; } = false;

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
