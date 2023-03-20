using Carwale.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Carwale.Objects
{
    public class CarDto
    {        
        public string UId { get; set; }
        public string TenantUId { get; set; }

        public int Year { get; set; }
      
        public string ModelUId { get; set; }

        public string ModelName { get; set; }

        public string MakeUId { get; set; }

        public string MakeName { get; set; }

        public string RegistrationNumber { get; set; }

        public string? Colour { get; set; }

        public double CurrentValue { get; set; }

        public double DepPercentage { get; set; }

        public double LeftCurrentVaile
        {
            get;
            //{
                //return CurrentValue - (CurrentValue * (DateTime.UtcNow.Year - Year) * DepPercentage*.01);
			//}
                set;
        }

        public byte Status { get; set; }

        public string? Notes { get; set; }


        public string? CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

    }
}
