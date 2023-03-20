using Carwale.Domain.Entities;

namespace Carwale.Objects
{
    public class AuthenticateResponse
    {
        public string UId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string TenantName { get; set; }
        public string TenantUId { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(string userUId, string name, string username, string tenantUId, string tenantName, string token)
        {
            UId = userUId;
            Name = name;
            Username = username;
            TenantUId = tenantUId;
            TenantName = tenantName;
            Token = token;
        }
    }
}
