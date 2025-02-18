namespace ExpressVoitures.Dto
{
    public class TypeDto
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public decimal Prix { get; set; }

        public decimal Duree { get; set; }

        public virtual ICollection<ReparationDto> Reparations { get; set; }
    }
}