using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwale.Domain.Entities
{
    [Table("cw_model")]
    [Index("MakeId", "Name", IsUnique = true)]
    public class Model : BaseEntity
    {
        public int MakeId { get; set; }

        public Make Make { get; set; }

        public string Name { get; set; }
    }
}
