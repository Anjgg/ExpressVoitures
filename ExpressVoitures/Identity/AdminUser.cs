using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ExpressVoitures.Identity
{
    public class AdminUser : IdentityUser
    {
        // Propriétés personnalisées ajoutées au modèle d'utilisateur par défaut
        public string Prenom { get; set; } = "Admin";
        public string Nom { get; set; } = "Admin";
        public bool IsActive { get; set; }
    }
}