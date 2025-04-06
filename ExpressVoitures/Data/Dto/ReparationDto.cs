using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ExpressVoitures.Data.Dto
{
    public class ReparationDto
    {
        [Key]
        public int Id { get; set; }

        public required string Description { get; set; }
        public decimal Prix { get; set; }
        public decimal Duree { get; set; }

        [Required]
        [ForeignKey("Voiture")]
        public string? CodeVin { get; set; }
        public virtual VoitureDto? Voiture { get; set; } 
    }
}