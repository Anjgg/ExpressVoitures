namespace ExpressVoitures.Data.Models
{
    public class PrixModel
    {
        public int Id { get; set; }

        public int PrixAchat { get; set; }
        public int PrixReparation { get; set; }
        public int PrixVente { get; set; }
    }
}