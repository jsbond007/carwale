using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwale.Domain.Entities
{
    [Table("cw_tenant")]
    public class Tenant: BaseEntity
	{
		[Required]
		public string Name { get; set; }

		public List<CwUser> Users { get; set; }
	}
}
