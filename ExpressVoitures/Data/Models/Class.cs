//namespace ExpressVoitures.Data.Models
//{
//    public class VoitureModel
//    {
//        public string CodeVin { get; set; } = null!;
//        public string Marque { get; set; } = null!;
//        public string Modele { get; set; } = null!;
//        public string Finition { get; set; } = null!;
//        public int? Annee { get; set; }
//        public string? ImagePath { get; set; }

//        public ICollection<ReparationModel> Reparations { get; set; } = new List<ReparationModel>();
//        public PrixModel? Prix { get; set; }
//        public DateModel? Date { get; set; }
//    }

//    public class DateModel
//    {
//        public int Id { get; set; }
//        public string CodeVin { get; set; } = null!;
//        public DateTimeOffset DateAchat { get; set; }
//        public DateTimeOffset DateMiseEnVente { get; set; }
//        public DateTimeOffset? DateVente { get; set; }

//        public VoitureModel Voiture { get; set; } = null!;
//    }

//    public class PrixModel
//    {
//        public int Id { get; set; }
//        public string CodeVin { get; set; } = null!;
//        public decimal PrixAchat { get; set; }
//        public decimal PrixReparation { get; set; }
//        public decimal PrixVente { get; set; }

//        public VoitureModel Voiture { get; set; } = null!;
//    }

//    public class ReparationModel
//    {
//        public int Id { get; set; }
//        public string CodeVin { get; set; } = null!;
//        public VoitureModel Voiture { get; set; } = null!;
//        public ICollection<ReparationTypeModel> ReparationTypes { get; set; } = new List<ReparationTypeModel>();

//    }

//    public class ReparationTypeModel
//    {
//        public int Id { get; set; }
//        public int ReparationId { get; set; }
//        public int TypeId { get; set; }

//        public ReparationModel Reparation { get; set; } = null!;
//        public Type Type { get; set; } = null!;
//    }

//    public class TypeModel
//    {
//        public int Id { get; set; }
//        public string Nom { get; set; } = null!;
//        public decimal Prix { get; set; }
//        public decimal Duree { get; set; }

//        public ICollection<ReparationTypeModel> ReparationTypes { get; set; } = new List<ReparationTypeModel>();
//    }
//}
