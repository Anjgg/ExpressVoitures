using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Dto
{
    public class VoitureDto
    {
        [Key]
        public string CodeVin { get; set; }

        public string Marque { get; set; }

        public string Modele { get; set; }

        public string Finition { get; set; }

        public int Annee { get; set; }
        
        public int ReparationId { get; set; }
        public virtual ReparationDto Reparation { get; set; }

        public int PrixId { get; set; }
        public virtual PrixDto Prix { get; set; }
       
        public int DateId { get; set; }
        public virtual DateDto Date { get; set; }
    }
}
