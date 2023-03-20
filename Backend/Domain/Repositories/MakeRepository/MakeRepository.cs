using Carwale.Domain;
using Carwale.Domain.Entities;
using Carwale.Objects;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Carwale.Domain.Repositories.MakeRepository
{
    public class MakeRepository : BaseRepository<Make>, IMakeRepository
    {
        public MakeRepository(CwDbContext cwDbContext) : base(cwDbContext)
        {

        }

        public async Task<IEnumerable<MakeDto>> GetAll()
        {
            var data = from x in _dbContext.Makes
                       select new MakeDto
                       {
                           UId = x.UId,
                           MakeName = x.Name,
                           CreatedBy = x.CreatedBy,
                           CreatedDateTime = x.CreatedDateTime
                       };

            var makes = await data.ToListAsync();
            return makes;

        }
    }
}

