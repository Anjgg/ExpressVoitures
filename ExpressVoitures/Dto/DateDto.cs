using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressVoitures.Dto
{
    public class DateDto
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset DateAchat { get; set; }

        public DateTimeOffset DateMiseEnVente { get; set; }

        public DateTimeOffset DateVente { get; set; }

        public virtual VoitureDto Voiture { get; set; }
    }
}
