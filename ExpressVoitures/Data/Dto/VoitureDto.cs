namespace ExpressVoitures.Data.Dto
{
    public class VoitureDto
    {
        public int Id { get; set; }

        public string CodeVin { get; set; } = string.Empty;
        public string Marque { get; set; } = string.Empty;
        public string Modele { get; set; } = string.Empty;
        public string Finition { get; set; } = string.Empty;
        public DateTimeOffset DateFabrication { get; set; }
        public string? ImagePath { get; set; }

        public virtual ReparationDto Reparation { get; set; } = new ReparationDto();
        public virtual PrixDto Prix { get; set; } = new PrixDto();
        public virtual DateDto Date { get; set; } = new DateDto();
    }
}
