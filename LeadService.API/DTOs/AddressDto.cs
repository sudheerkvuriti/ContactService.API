namespace LeadService.API.DTOs
{
    public class AddressDto
    {
        public string AddressType { get; set; }
       
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string Pincode { get; set; }
    }
}
