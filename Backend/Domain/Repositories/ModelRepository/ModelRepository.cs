using Carwale.Domain;
using Carwale.Domain.Entities;
using Carwale.Objects;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Carwale.Domain.Repositories.ModelRepository
{
    public class ModelRepository : BaseRepository<Model>, IModelRepository
    {
        public ModelRepository(CwDbContext cwDbContext) : base(cwDbContext)
        {

        }

        public async Task<IEnumerable<ModelDto>> GetAll(string makeUId)
        {
            var data = from x in _dbContext.Models
                       select new ModelDto
                       {
                           UId = x.UId,
                           Name = x.Name,
                           MakeUId = x.Make.UId,
                           MakeName = x.Make.Name,
                           CreatedBy = x.CreatedBy,
                           CreatedDateTime = x.CreatedDateTime
                       };

            var makes = await data.Where(p=>p.MakeUId==makeUId).ToListAsync();
            return makes;

        }

        public async Task<Model> GetByUId(string uId)
        {
            var model = await _dbContext.Models.FirstOrDefaultAsync(i => i.UId == uId);
            return model;  
        }
    }
}

