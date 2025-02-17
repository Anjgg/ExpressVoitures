using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Dto
{
    public class VoitureDto
    {
        [Key]
        public string CodeVin { get; set; }

        public string Marque { get; set; }

        public string Modele { get; set; }

        public string Finition { get; set; }

        public int Annee { get; set; }

        [ForeignKey("OperationId")]
        public OperationDto OperationDto { get; set; }
    }
}
