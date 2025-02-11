using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Database.Dto
{
    public class ReparationDto
    {
        [Key]
        public int ReparationId { get; set; }

        public string Type { get; set; }

        public string Code { get; set; }

        public decimal Prix { get; set; }

        public double DureeJour { get; set; }

        public double DureeHeure { get; set; }

        public int OperationId { get; set; }
        public ICollection<OperationDto> OperationDtos { get; set; }
    }
}