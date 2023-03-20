namespace Carwale.Domain.Entities
{
    public interface IModifiableEntity
    {
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
