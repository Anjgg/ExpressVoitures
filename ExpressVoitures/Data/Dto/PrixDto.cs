﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Data.Dto
{
    public class PrixDto
    {
        [Key]
        public int Id { get; set; }

        public decimal PrixAchat { get; set; }
        public decimal PrixReparation { get; set; }
        public decimal PrixVente { get; set; }

        [ForeignKey("Voiture")]
        public string? CodeVin { get; set; }
        public virtual VoitureDto? Voiture { get; set; } 
    }
}
