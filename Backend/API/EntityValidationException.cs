using System.ComponentModel.DataAnnotations;
using Carwale.Objects;

namespace Carwale.API
{
    public class EntityValidationException : Exception
    {
        public ApiResponse<string> ApiResponse { get; set; }
        public EntityValidationException(string message, IEnumerable<IValidationError> validationErrors) : base(message)
        {
            ApiResponse = new ApiResponse<string>(validationErrors);
        }
        public EntityValidationException(string message, IValidationError validationError) : base(message)
        {
            ApiResponse = new ApiResponse<string>(new List<IValidationError> { validationError });
        }
    }
}
