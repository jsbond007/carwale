namespace Carwale.Domain.Entities
{
    public class UIdGenerator
    {

        private static Random random = new Random();
        public static string GenerateUId(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
