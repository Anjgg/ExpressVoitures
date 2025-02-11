using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Database.Dto
{
    public class OperationDto
    {
        [Key]
        public int OperationId { get; set; }
        
        public VoitureDto VoitureDto { get; set; }

        public ICollection<ReparationDto> ReparationDtos { get; set; }

        public DateTimeOffset DateAchat { get; set; }

        public DateTimeOffset DateMiseEnVente { get; set; }

        public DateTimeOffset DateVente { get; set; }

        public decimal PrixAchat { get; set; }

        public decimal PrixReparation { get; set; }

        public decimal PrixVente { get; set; }
    }
}
