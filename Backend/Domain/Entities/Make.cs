using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwale.Domain.Entities
{
	[Table("cw_make")]
    [Index("Name", IsUnique = true)]
    public class Make:BaseEntity
	{
		

		public string Name { get; set; }

		public List<Model> Models { get; set; }

	}
}
