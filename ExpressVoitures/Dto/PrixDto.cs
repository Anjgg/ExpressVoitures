using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Dto
{
    public class PrixDto
    {
        [Key]
        public int PrixId { get; set; }

        public decimal PrixAchat { get; set; }

        public decimal PrixReparation { get; set; }

        public decimal PrixVente { get; set; }

        [ForeignKey("OperationId")]
        public OperationDto Operation { get; set; }
    }
}
