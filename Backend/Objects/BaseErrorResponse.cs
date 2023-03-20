namespace Carwale.Objects
{
    public class BaseErrorResponse
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
