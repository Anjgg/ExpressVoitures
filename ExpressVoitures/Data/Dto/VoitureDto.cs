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

        [ForeignKey("Reparation")]
        public int ReparationId { get; set; }
        public virtual ReparationDto? Reparation { get; set; }

        [ForeignKey("Prix")]
        public int PrixId { get; set; }
        public virtual PrixDto? Prix { get; set; }

        [ForeignKey("Date")]
        public int DateId { get; set; }
        public virtual DateDto? Date { get; set; }
    }
}
