namespace WebAPI.DTOs
{
    public class CustomerResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
    }
}
