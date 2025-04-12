namespace ExpressVoitures.Data.Dto
{
    public class DateDto
    {
        public int Id { get; set; }

        public DateTimeOffset DateAchat { get; set; }
        public DateTimeOffset DateMiseEnVente { get; set; }
        public DateTimeOffset? DateVente { get; set; }

        public int? VoitureId { get; set; }
        public virtual VoitureDto? Voiture { get; set; }
    }
}
