using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Data.Dto
{
    public class TypeDto
    {
        [Key]
        public int Id { get; init; }

        public required string Nom { get; set; }
        public decimal Prix { get; set; }
        public decimal Duree { get; set; }

        public virtual ICollection<ReparationTypeDto> ReparationTypes { get; set; } = new List<ReparationTypeDto>();
    }
}