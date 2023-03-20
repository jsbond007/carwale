using Carwale.Objects;

namespace Carwale.Services.UserService
{
    public interface IUserService
    {
        Task<BaseResponse> Authenticate(string username, string password);
    }
}
