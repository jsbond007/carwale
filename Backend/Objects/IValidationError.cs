namespace Carwale.Objects
{
    public interface IValidationError
    {

        public int Code { get; set; }

        public string Message { get; set; }

    }
}
