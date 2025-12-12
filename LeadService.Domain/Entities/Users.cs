using System.Collections.Generic;

namespace LeadService.Domain.Entities
{
    public class User
    {


        public virtual string UserID { get; set; }
        public virtual string Title { get; set; }
        public virtual string FullName { get; set; }
        public virtual string EmailID { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string Address { get; set; }
        public virtual string Password { get; set; }
        public virtual int RoleID { get; set; }
        //public virtual int GroupID { get; set; }
        public virtual int Capacity { get; set; }
        public virtual int TimeZoneID { get; set; }
        public virtual string Signature { get; set; }
        public virtual byte[] Photo { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string ReportTo { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual Nullable<DateTime> UpdatedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual string DeletedBy { get; set; }
        public virtual Nullable<DateTime> DeletedDate { get; set; }
        public virtual int TitleID { get; set; }
        public virtual Nullable<DateTime> LastLoginDate { get; set; }
        public virtual int CallCenterID { get; set; }
        public virtual bool IsLoggedInFlag { get; set; }
       
        public virtual int OrganizationID { get; set; }
       
        public virtual string Grade { get; set; }
        public virtual string UserCode { get; set; }
        public virtual int? SourceTypeID { get; set; }
        public virtual string UserType { get; set; }

    }
}
