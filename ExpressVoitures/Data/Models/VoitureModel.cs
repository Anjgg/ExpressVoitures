﻿namespace ExpressVoitures.Data.Models
{
    public class VoitureModel
    {
        public int Id { get; set; }

        public string CodeVin { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public string Finition { get; set; } = null!;
        public DateTime Annee { get; set; }
        public string? ImagePath { get; set; }
    }
}
