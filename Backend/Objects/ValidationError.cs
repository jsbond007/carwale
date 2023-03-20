using Microsoft.VisualBasic;

namespace Carwale.Objects
{
    public class ValidationError : IValidationError
    {
        public ValidationError(string message,int code )
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }

        public string Message { get; set; }

    }
}
