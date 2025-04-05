using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Data.Dto
{
    public class ReparationTypeDto
    {
        [Key]
        public int Id { get; init; }

        [ForeignKey("Reparation")]
        public int ReparationId { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }

        public virtual ReparationDto Reparation { get; set; } = null!;
        public virtual TypeDto Type { get; set; } = null!;
    }
}
