using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Le code VIN est obligatoire.")]
        [Display(Name = "Code VIN")]
        [RegularExpression(@"^[A-Z0-9]{16}$", ErrorMessage = "Le code VIN doit contenir 16 caractères alphanumériques.")]
        [StringLength(17, ErrorMessage = "Le code VIN doit contenir 16 caractères.")]
        public string CodeVin { get; set; } = null!;

        [Required(ErrorMessage = "La marque est obligatoire.")]
        [Display(Name = "Marque")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "La marque ne doit contenir que des lettres, des chiffres et des espaces.")]
        [StringLength(50, ErrorMessage = "La marque ne doit pas dépasser 50 caractères.")]
        public string Marque { get; set; } = null!;

        [Required(ErrorMessage = "Le modèle est obligatoire.")]
        [Display(Name = "Modèle")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Le modèle ne doit contenir que des lettres, des chiffres et des espaces.")]
        [StringLength(50, ErrorMessage = "Le modèle ne doit pas dépasser 50 caractères.")]
        public string Modele { get; set; } = null!;

        [Required(ErrorMessage = "La finition est obligatoire.")]
        [Display(Name = "Finition")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "La finition ne doit contenir que des lettres, des chiffres et des espaces.")]
        [StringLength(50, ErrorMessage = "La finition ne doit pas dépasser 50 caractères.")]
        public string Finition { get; set; } = null!;

        [Required(ErrorMessage = "L'année de fabrication est obligatoire.")]
        [Display(Name = "Année de fabrication")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTimeOffset), "1950-01-01", "1990-12-31", ErrorMessage = "L'année de fabrication doit être comprise entre 1950 et 1990.")]
        public DateTimeOffset AnneeFabrication { get; set; }

        public string? ImagePath { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".jpeg" })]
        [DataType(DataType.Upload)]
        public IFormFile? ImageVoiture { get; set; }
    }

    public class DateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La date d'achat est obligatoire.")]
        [Display(Name = "Date d'achat")]
        [DataType(DataType.Date)]
        public DateTimeOffset DateAchat { get; set; }

        [Display(Name = "Date de mise en vente")]
        [DataType(DataType.Date)]
        public DateTimeOffset DateMiseEnVente { get; set; }

        [Display(Name = "Date de vente")]
        [DataType(DataType.Date)]
        public DateTimeOffset? DateVente { get; set; }
    }

    public class PrixModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le prix d'achat est obligatoire.")]
        [Display(Name = "Prix d'achat")]
        [Range(0, int.MaxValue, ErrorMessage = "Le prix d'achat doit être un nombre positif.")]
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

        [Required(ErrorMessage = "Le type est obligatoire.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Le prix est obligatoire.")]
        [Display(Name = "Prix")]
        [Range(0, int.MaxValue, ErrorMessage = "Le prix doit être un nombre positif.")]
        public int Prix { get; set; }

        [Required(ErrorMessage = "La durée est obligatoire.")]
        [Display(Name = "Durée")]
        [Range(0, double.MaxValue, ErrorMessage = "La durée doit être un nombre positif.")]
        public double Duree { get; set; }

        public bool IsSelected { get; set; } = false;
    }
}
