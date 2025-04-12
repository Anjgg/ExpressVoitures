namespace ExpressVoitures.Data.Dto
{
    public class EventHistoryDto
    {
        public int Id { get; set; }

        public DateTimeOffset Purchase { get; set; }
        public DateTimeOffset ReadyToSell { get; set; }
        public DateTimeOffset? Selling { get; set; }

        public int CarId { get; set; } 
    }
}
