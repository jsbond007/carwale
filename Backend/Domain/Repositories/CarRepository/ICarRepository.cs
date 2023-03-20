using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carwale.Domain.Entities;
using Carwale.Objects;

namespace Carwale.Domain.Repositories.CarRepository
{
    public interface ICarRepository : IRepository<Car>
    {
		Task<Car> GetEntity(string uid, string tenantUId);
		Task<IEnumerable<CarDto>> GetAll(int? status,string tenantUId);
        Task<CarDto> GetDetailByUId(string uid, string tenantUId);
    }
}
