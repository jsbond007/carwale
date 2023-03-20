using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carwale.Domain.Entities;
using Carwale.Objects;

namespace Carwale.Domain.Repositories.MakeRepository
{
    public interface IMakeRepository
    {
        Task<IEnumerable<MakeDto>> GetAll();
    }
}
