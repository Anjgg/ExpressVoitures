namespace ExpressVoitures.Data.Entities
{
    public class Price
    {
        public int Id { get; set; }

        public decimal Purchase { get; set; }
        public decimal Repair { get; set; }
        public decimal Selling { get; set; }

        public int CarId { get; set; }
        public virtual Car? Car { get; set; }
    }
}
