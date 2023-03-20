using Carwale.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Carwale.Objects
{
    public class MakeDto
    {
        public string UId { get; set; }

        public string MakeName { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

    }
}
