using LeadService.Domain.Enums;

namespace LeadService.Domain.Entities
{
    public class Address
    {
        public int AddressID { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public string ZipCode { get; set; }
        public int CountryID { get; set; }

        public string AddressLine { get; set; }
        public AddressType AddressType { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsDefault { get; set; }
        public bool IsBilling { get; set; }
        public bool IsCommunication { get; set; }
        public bool IsShipping { get; set; }
        public int OrganizationID { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        // Navigation
        public ICollection<LeadAddress> LeadAddresses { get; set; }
    }
}
