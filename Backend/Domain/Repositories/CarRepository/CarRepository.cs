using Carwale.Domain;
using Carwale.Domain.Entities;
using Carwale.Objects;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Carwale.Domain.Repositories.CarRepository
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(CwDbContext cwDbContext) : base(cwDbContext)
        {

        }

		/// <summary>
		/// This will select Domain Entity object for given uid and tenantUId
		/// </summary>
		/// <param name="uid">Unique Id of car</param>
		/// <param name="tenantUId">Tenant UId of the user</param>
		/// <returns></returns>
        public async Task<Car> GetEntity(string uid, string tenantUId)
        {
            return await this._dbContext.Set<Car>().Where(p => p.Tenant.UId == tenantUId && p.UId == uid).FirstOrDefaultAsync();
        }

		private IQueryable<CarDto> GetQueryable(string tenantUId)
        {
			var data = from x in _dbContext.Cars.AsNoTracking()
					   select new CarDto
					   {
						   UId = x.UId,
						   Colour = x.Colour,
						   CurrentValue = x.CurrentValue,
						   CreatedBy = x.CreatedBy,
						   CreatedDateTime = x.CreatedDateTime,
						   MakeName = x.Model.Make.Name,
						   MakeUId = x.Model.Make.UId,
						   ModelName = x.Model.Name,
						   ModelUId = x.Model.UId,
						   ModifiedBy = x.ModifiedBy,
						   ModifiedDateTime = x.ModifiedDateTime,
						   Notes = x.Notes,
						   RegistrationNumber = x.RegistrationNumber,
						   Status = x.Status,
						   TenantUId = tenantUId,
                           LeftCurrentVaile= Math.Round(x.CurrentValue - (x.CurrentValue * (DateTime.UtcNow.Year - (x.Year>= DateTime.UtcNow.Year? DateTime.UtcNow.Year:x.Year)) * 5 * .01),0),     //5 is default dep percentage 
						   Year = x.Year
					   };

            return data.Where(p => p.TenantUId == tenantUId);
		}

		/// <summary>
		/// Get all Cars for given Status and Tenant UId
		/// </summary>
		/// <param name="status">Status could one of EnumCarStatus value as Integer</param>
		/// <param name="tenantUId">Tenant UId of the user</param>
		/// <returns></returns>
		public async Task<IEnumerable<CarDto>> GetAll(int? status, string tenantUId)
		{			
			var cars = await GetQueryable(tenantUId).Where(p=> (status == null || p.Status == status)).ToListAsync();
			return cars;
		}

		public async Task<CarDto> GetDetailByUId(string uid, string tenantUId)
        {   
            var result = await GetQueryable(tenantUId).Where(p=>p.UId == uid).FirstOrDefaultAsync();
            return result;

        }
    }
}

