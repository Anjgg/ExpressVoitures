
namespace ExpressVoitures.Models
{
    public class DateModel
    {
        public int DateId { get; set; }

        public DateTimeOffset DateAchat { get; set; }

        public DateTimeOffset DateMiseEnVente { get; set; }

        public DateTimeOffset DateVente { get; set; }
    }
}
