namespace ExpressVoitures.Data.Dto
{
    public class TypeDto
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;
        public int Prix { get; set; }
        public double Duree { get; set; }

        public virtual IEnumerable<ReparationDto> Reparations { get; set; } = new List<ReparationDto>();
    }
}
