using System.Security.Claims;
using System.Security.Principal;

namespace Carwale.UserIdentity
{
    public class CWUserIdentity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CWUserIdentity(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == true)
            {
                UserName = GetClaimValue("UserName");
                IsAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                TenantId = int.Parse(GetClaimValue("TenantId"));
                TenantUId = GetClaimValue("TenantUId");
            }
            else
            {
                TenantId = 1;
                TenantUId = "GL3EL54OV8";
                UserName = "muser1";
                Name = "Manoj XBoss";
            }

        }

        public string GetClaimValue(string ClaimName)
        {
            if (_httpContextAccessor.HttpContext == null)
                return string.Empty;

            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(c => c.Type == ClaimName);
            if (claim != null)
            {
                return claim.Value;
            }
            return string.Empty;
        }

        public string Name { get; set; }

        public string UserName { get; set; }

        public bool IsAuthenticated { get; private set; }

        public string TenantUId { get; set; }

        public int TenantId { get; set; }

    }
}
