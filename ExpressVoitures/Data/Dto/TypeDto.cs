namespace ExpressVoitures.Data.Dto
{
    public class TypeDto
    {
        public int Id { get; init; }

        public string Nom { get; set; }

        public decimal Prix { get; set; }

        public decimal Duree { get; set; }

        public virtual ICollection<ReparationDto> Reparations { get; set; }
    }
}