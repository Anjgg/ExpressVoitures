using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Database.Dto
{
    public class VoitureDto
    {
        [Key]
        public string CodeVin { get; set; }

        public string Marque { get; set; }

        public string Modele { get; set; }

        public string Finition { get; set; }

        public int Annee { get; set; }

        public int OperationId { get; set; }
        public OperationDto OperationDto { get; set; }
    }
}
