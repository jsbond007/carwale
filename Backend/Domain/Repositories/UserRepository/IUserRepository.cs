using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carwale.Domain.Entities;
using Carwale.Objects;

namespace Carwale.Domain.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<CwUser> Login(string username, string password);
    }
}
