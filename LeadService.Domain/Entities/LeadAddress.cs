namespace LeadService.Domain.Entities
{
    public class LeadAddress
    {
        public int Id { get; set; }

        public int LeadID { get; set; }
        public Lead Lead { get; set; }

        public int AddressID { get; set; }
        public Address Address { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AddressType { get; set; }

        
    }
}
