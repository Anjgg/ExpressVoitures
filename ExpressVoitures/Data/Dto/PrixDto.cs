namespace ExpressVoitures.Data.Dto
{
    public class PrixDto
    {
        public int Id { get; set; }

        public int PrixAchat { get; set; }
        public int PrixReparation { get; set; }
        public int PrixVente { get; set; }

        public int? VoitureId { get; set; }
        public virtual VoitureDto? Voiture { get; set; }
    }
}
