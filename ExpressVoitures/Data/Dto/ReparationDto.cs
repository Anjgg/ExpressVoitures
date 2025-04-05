using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Data.Dto
{
    public class ReparationDto
    {
        [Key]
        public int Id { get; init; }

        public virtual VoitureDto Voiture { get; set; } = null!;
        public virtual ICollection<ReparationTypeDto> ReparationTypes { get; set; } = new List<ReparationTypeDto>();
        
    }
}