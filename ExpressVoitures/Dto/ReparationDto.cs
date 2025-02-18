using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Dto
{
    public class ReparationDto
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<TypeDto> Types { get; set; }

        public virtual ICollection<VoitureDto> Voitures { get; set; }
    }
}