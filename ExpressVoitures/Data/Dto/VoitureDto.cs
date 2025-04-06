using Humanizer.Bytes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Data.Dto
{
    public class VoitureDto
    {
        [Key]
        public required string CodeVin { get; set; }
        public required string Marque { get; set; }
        public required string Modele { get; set; }
        public required string Finition { get; set; }
        public int? Annee { get; set; }
        public string? ImagePath { get; set; }

        public virtual IList<ReparationDto> Reparations { get; set; } = new List<ReparationDto>();
        public virtual PrixDto Prix { get; set; } = new PrixDto();
        public virtual DateDto Date { get; set; } = new DateDto();
    }
}
