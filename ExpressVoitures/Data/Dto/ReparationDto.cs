namespace ExpressVoitures.Data.Dto
{
    public class ReparationDto
    {
        public int Id { get; set; }

        public int PrixTotal { get; set; }
        public double DureeTotal { get; set; }

        public int? VoitureId { get; set; }
        public virtual VoitureDto? Voiture { get; set; }

        public virtual IEnumerable<TypeDto> Types { get; set; } = new List<TypeDto>();
    }
}