namespace ExpressVoitures.Data.Entities
{
    public class EventHistory
    {
        public int Id { get; set; }

        public DateTimeOffset Purchase { get; set; }
        public DateTimeOffset ReadyToSell { get; set; }
        public DateTimeOffset? Selling { get; set; }

        public int CarId { get; set; }
        public virtual Car? Car { get; set; }
    }
}
