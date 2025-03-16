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

        public int Annee { get; set; }

        public byte[]? Image { get; set; }

        public virtual ReparationDto? Reparation { get; set; }

        public int PrixId { get; set; }
        public virtual PrixDto Prix { get; set; } = new PrixDto();

        public int DateId { get; set; }
        public virtual DateDto Date { get; set; } = new DateDto();
    }
}
