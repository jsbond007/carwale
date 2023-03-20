using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwale.Domain.Entities
{
    [Table("cw_user")]
    [Index("UserName", IsUnique = true)]
    public class CwUser : BaseEntity, IMultiTenantEntity
    {
        [Required]
        public int TenantId { get; set; }

        public Tenant Tenant { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
