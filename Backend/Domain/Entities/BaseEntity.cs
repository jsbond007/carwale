using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Carwale.Domain.Entities
{
    [Index("UId", IsUnique = true)]
    public abstract class BaseEntity
	{
		public BaseEntity()
		{
			this.CreatedBy = "System";
			this.CreatedDateTime = DateTime.UtcNow;
		}

		[Key]		
		public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string UId { get; set; }

        public string CreatedBy { get; set; }

		public DateTime CreatedDateTime { get; set; }


	}
}
