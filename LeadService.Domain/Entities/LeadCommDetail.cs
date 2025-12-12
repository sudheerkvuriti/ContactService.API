namespace LeadService.Domain.Entities
{
    public class LeadCommDetail
    {
        public int Id { get; set; }

        public int LeadID { get; set; }
        public Lead Lead { get; set; }

        public int CommTypeID { get; set; } // e.g., Mobile, Email
        public string CommSubTypeName { get; set; }
        public string CommTypeValue { get; set; }

        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
