using System.ComponentModel.DataAnnotations;

namespace Carwale.Domain.Entities
{
    public interface IMultiTenantEntity
    {        
        public int TenantId { get; set; }

    }
}
