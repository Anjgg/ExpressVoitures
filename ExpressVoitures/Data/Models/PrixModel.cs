using ExpressVoitures.Data.Dto;

namespace ExpressVoitures.Data.Models
{
    public class PrixModel
    {
        public int Id { get; set; }
        
        public decimal PrixAchat { get; set; }
        public decimal PrixReparation { get; set; }
        public decimal PrixVente { get; set; }

        public void SetPrixVente(VoitureModel voiture)
        {
            PrixVente = voiture.Reparations.Sum(r => r.Prix) + PrixAchat + 500;
        }
    }
}