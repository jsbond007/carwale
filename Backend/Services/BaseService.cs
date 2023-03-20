using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Carwale.UserIdentity;

namespace Carwale.Services
{
    public abstract class BaseService
    {
        private readonly IMapper _mapper;
        private readonly CWUserIdentity _userIdentity;
        public BaseService(IMapper mapper, CWUserIdentity userIdentity)
        {
            _mapper = mapper;
            _userIdentity = userIdentity;
        }

        public TDestination Map<TDestination> (object from)
        {
            return _mapper.Map<TDestination>(from);
        }

        public CWUserIdentity User
        {
            get
            {
                return _userIdentity; 
            }
        }
    }
}
