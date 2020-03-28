namespace RestBnb.Core.Entities
{
    public class GetAllPropertiesFilter
    {
        public decimal MaxPricePerNight { get; set; }
        public decimal MinPricePerNight { get; set; }
        public int AccommodatesNumber { get; set; }
        public int UserId { get; set; }
    }
}