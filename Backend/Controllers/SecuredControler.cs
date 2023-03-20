using Carwale.UserIdentity;
using Microsoft.AspNetCore.Authorization;

namespace Carwale.Controllers
{
    [Authorize]
    public class SecuredControler : BaseController
    {
        public CWUserIdentity User { get; set; }
    }
}
