using LeadService.API.DTOs;

public class LeadCreateDto
{
    public string LeadName { get; set; }
    public int LeadSourceID { get; set; }

    public AddressDto Address { get; set; }

    // Communication
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? AlternatePhone { get; set; }
}
