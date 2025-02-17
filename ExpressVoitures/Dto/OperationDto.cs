using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Dto
{
    public class OperationDto
    {
        [Key]
        public int OperationId { get; set; }

        public DateDto Date { get; set; }

        public PrixDto Prix { get; set; }

        public VoitureDto VoitureDto { get; set; }

        public ICollection<ReparationDto> ReparationDtos { get; set; }
    }
}
