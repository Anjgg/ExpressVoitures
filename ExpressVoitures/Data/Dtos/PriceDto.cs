namespace ExpressVoitures.Data.Dto
{
    public class PriceDto
    {
        public int Id { get; set; }

        public decimal Purchase { get; set; }
        public decimal Repair { get; set; }
        public decimal Selling { get; set; }

        public int CarId { get; set; }
    }
}
