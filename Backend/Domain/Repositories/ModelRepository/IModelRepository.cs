using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carwale.Domain.Entities;
using Carwale.Objects;

namespace Carwale.Domain.Repositories.ModelRepository
{
    public interface IModelRepository
    {
        Task<IEnumerable<ModelDto>> GetAll(string makeUId);
        Task<Model> GetByUId(string uId);
    }
}
