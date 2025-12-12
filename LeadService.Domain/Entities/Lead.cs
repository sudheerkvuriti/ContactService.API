namespace LeadService.Domain.Entities
{
    public class Lead
    {
        public int LeadID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SalutationID { get; set; }
        public int? LeadSourceID { get; set; }
        public int? LeadRatingID { get; set; }
        public int? LeadStatusID { get; set; }
        public int? IndustryTypeID { get; set; }
        public int? JobTitleID { get; set; }
        public int? LeadOrganizationID { get; set; }
        public int? OwnerID { get; set; }

        public string Description { get; set; }
        public bool AssignToTeam { get; set; }
        public bool AssignToAgent { get; set; }

        public int Age { get; set; }
        public DateTime? BirthDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsMerged { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserType { get; set; }

        // Navigation
        public ICollection<LeadAddress> LeadAddresses { get; set; }
        public ICollection<LeadCommDetail> LeadCommDetails { get; set; }
    }
}
