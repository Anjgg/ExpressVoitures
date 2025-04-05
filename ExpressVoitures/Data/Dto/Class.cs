//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ExpressVoitures.Data.Dto
//{
//    public class VoitureDto
//    {
//        [Key]
//        public required string CodeVin { get; set; }
//        public required string Marque { get; set; }
//        public required string Modele { get; set; }
//        public required string Finition { get; set; }
//        public int? Annee { get; set; }
//        public string? ImagePath { get; set; }

//        public virtual ICollection<ReparationDto?> Reparations { get; set; } = new List<ReparationDto?>();

//        [ForeignKey("Prix")]
//        public int? PrixId { get; set; }
//        public virtual PrixDto? Prix { get; set; }

//        [ForeignKey("Date")]
//        public int? DateId { get; set; }
//        public virtual DateDto? Date { get; set; }
//    }

//    public class DateDto
//    {
//        [Key]
//        public int Id { get; init; }

//        [ForeignKey("Voiture")]
//        public string CodeVin { get; set; }

//        public DateTimeOffset DateAchat { get; set; }
//        public DateTimeOffset DateMiseEnVente { get; set; }
//        public DateTimeOffset? DateVente { get; set; }

//        public virtual VoitureDto Voiture { get; set; } = null!;
//    }

//    public class PrixDto
//    {
//        [Key]
//        public int Id { get; init; }

//        [ForeignKey("Voiture")]
//        public string CodeVin { get; set; }

//        public decimal PrixAchat { get; set; }
//        public decimal PrixReparation { get; set; }
//        public decimal PrixVente { get; set; }

//        public virtual VoitureDto Voiture { get; set; } = null!;
//    }

//    public class ReparationDto
//    {
//        [Key]
//        public int Id { get; init; }

//        [ForeignKey("Voiture")]
//        public string CodeVin { get; set; }

//        public virtual VoitureDto Voiture { get; set; } = null!;
//        public virtual ICollection<ReparationTypeDto> ReparationTypes { get; set; } = new List<ReparationTypeDto>();

//    }

//    public class ReparationTypeDto
//    {
//        [Key]
//        public int Id { get; init; }

//        [ForeignKey("Reparation")]
//        public int ReparationId { get; set; }

//        [ForeignKey("Type")]
//        public int TypeId { get; set; }

//        public virtual ReparationDto Reparation { get; set; } = null!;
//        public virtual TypeDto Type { get; set; } = null!;
//    }

//    public class TypeDto
//    {
//        [Key]
//        public int Id { get; init; }

//        public required string Nom { get; set; }
//        public decimal Prix { get; set; }
//        public decimal Duree { get; set; }

//        public virtual ICollection<ReparationTypeDto> ReparationTypes { get; set; } = new List<ReparationTypeDto>();
//    }

//}
