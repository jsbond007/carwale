using Carwale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carwale.Objects;
using AutoMapper;
using Carwale.UserIdentity;
using Carwale.API;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Carwale.Domain.Repositories.UserRepository;

namespace Carwale.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        public UserService(IMapper mapper,
            CWUserIdentity userIdentity,
            IUserRepository userRepository,
            JwtService jwtService)
            : base(mapper, userIdentity)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Will check if username and password exists in the database and will return the JWT token if exists
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns AuthenticateResponse if Successful with Token</returns>
        public async Task<BaseResponse> Authenticate(string username, string password)
        {
            var user = await _userRepository.Login(username, password);
            if (user == null) return ApiResponse<string>.ErrorResponse("User or password is incorect found");
            
            var token = _jwtService.GenerateJwtToken(user);

            var response=new   AuthenticateResponse(user.UId, user.Name, user.UserName, user.Tenant.UId, user.Tenant.Name, token);
            return ApiResponse<AuthenticateResponse>.SuccessResponse(response);
        }
    }
}
