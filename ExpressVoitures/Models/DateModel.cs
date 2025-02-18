
namespace ExpressVoitures.Models
{
    public class DateModel
    {
        public int Id { get; set; }

        public DateTimeOffset DateAchat { get; set; }

        public DateTimeOffset DateMiseEnVente { get; set; }

        public DateTimeOffset DateVente { get; set; }

        public VoitureModel Voiture { get; set; }
    }
}
