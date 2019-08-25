using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emando.Vantage.Entities.Identity
{
    public class RefreshToken
    {
        [Key]
        [MaxLength(50)]
        public string TokenHash { get; set; }

        public virtual ClientApplication Client { get; set; }

        [StringLength(100)]
        public string GrantType { get; set; }
        
        [StringLength(256)]
        public string Subject { get; set; }
        
        [StringLength(128)]
        public string ClientId { get; set; }

        public DateTime Issued { get; set; }

        [Index]
        public DateTime Expires { get; set; }
        
        [Required]
        public string ProtectedTicket { get; set; }
    }
}