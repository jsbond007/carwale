using Carwale.Domain;
using Carwale.Domain.Entities;
using Carwale.Objects;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Carwale.Domain.Repositories.UserRepository
{
    public class UserRepository : BaseRepository<CwUser>, IUserRepository
    {
        public UserRepository(CwDbContext cwDbContext) : base(cwDbContext)
        {

        }

        public async Task<CwUser> Login(string username, string password)
        {
            var user = await _dbContext.Users.Include(x=>x.Tenant).Where(i => i.UserName == username && i.Password == password).FirstOrDefaultAsync();
            return user;

        }
    }
}

