namespace RestBnb.API.Application.PropertyImages.Responses
{
    public class PropertyImageResponse
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int PropertyId { get; set; }
    }
}
