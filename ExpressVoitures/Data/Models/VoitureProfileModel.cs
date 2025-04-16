namespace ExpressVoitures.Data.Models
{
    public class VoitureProfileModel
    {
        public VoitureModel Voiture { get; set; } = new VoitureModel();
        public DateModel Date { get; set; } = new DateModel();
        public PrixModel Prix { get; set; } = new PrixModel();
        public ReparationModel Reparation { get; set; } = new ReparationModel();
        public List<TypeModel> Types { get; set; } = new List<TypeModel>();
    }

    public class VoitureModel
    {
        public int Id { get; set; }

        public string CodeVin { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public string Finition { get; set; } = null!;
        public DateTimeOffset AnneeFabrication { get; set; }
        public string? ImagePath { get; set; }
    }

    public class DateModel
    {
        public int Id { get; set; }

        public DateTimeOffset DateAchat { get; set; }
        public DateTimeOffset DateMiseEnVente { get; set; }
        public DateTimeOffset? DateVente { get; set; }
    }

    public class PrixModel
    {
        public int Id { get; set; }

        public int PrixAchat { get; set; }
        public int PrixReparation { get; set; }
        public int PrixVente { get; set; }
    }

    public class ReparationModel
    {
        public int Id { get; set; }

        public int PrixTotal { get; set; }
        public double DureeTotal { get; set; }
    }

    public class TypeModel
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;
        public int Prix { get; set; }
        public double Duree { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
