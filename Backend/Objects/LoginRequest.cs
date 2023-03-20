using System.ComponentModel.DataAnnotations;

namespace Carwale.Objects
{
	public class LoginRequest
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}

}
