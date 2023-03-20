using Carwale.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Carwale.Objects
{
	public class CarCreateRequest
	{
		[Required]
        [Range(1900, 2999)]
        public int Year { get; set; }
		
		[Required(ErrorMessage ="Model is required")]
		public string ModelUId { get; set; }

		
		[MaxLength(20)]
		public string RegistrationNumber { get; set; }

		public string? Colour { get; set; }

		[Range(1000,1000000000)]
		public double CurrentValue { get; set; }
		
		[MaxLength(1000)]
		public string? Notes { get; set; }

	}
}
