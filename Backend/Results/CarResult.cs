using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwale.Results
{
    public class CarResult
    {
		public int Id { get; set; }
		public string UId { get; set; }
		public int TenantId { get; set; }
		public int Year { get; set; }
		public int ModelId { get; set; }

		public string RegistrationNumber { get; set; }

		public string? Colour { get; set; }

		public double CurrentValue { get; set; }

		public byte Status { get; set; }
		public string? Notes { get; set; }

		public string? ModifiedBy { get; set; }

		public DateTime? ModifiedDateTime { get; set; }

		public string CreatedBy { get; set; }

		public DateTime CreatedDateTime { get; set; }
	}
}
