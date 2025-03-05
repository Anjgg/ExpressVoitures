using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Data.Dto
{
    public class ReparationDto
    {
        [Key]
        public int Id { get; init; }

        public virtual ICollection<TypeDto> Types { get; set; } = new List<TypeDto>();

        public string CodeVin { get; set; }
        public virtual ICollection<VoitureDto> Voitures { get; set; }
    }
}