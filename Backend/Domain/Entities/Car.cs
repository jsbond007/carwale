using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwale.Domain.Entities
{
    [Table("cw_car")]
    public class Car : BaseEntity, IModifiableEntity, IMultiTenantEntity
    {
        [Required]
        public int TenantId { get; set; }

        public Tenant Tenant { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int ModelId { get; set; }

        public Model Model { get; set; }

        [MaxLength(20)]
        public string RegistrationNumber { get; set; }

        public string? Colour { get; set; }

        public double CurrentValue { get; set; }

        public byte Status { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

    }
}
