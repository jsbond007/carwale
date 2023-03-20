using Carwale.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Carwale.Objects
{
    public class ModelDto
    {
        public string UId { get; set; }

        public string Name { get; set; }


        public string MakeUId { get; set; }
        public string MakeName { get; set; }


        public string? CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

    }
}
