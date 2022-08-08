using System;

namespace Store.Models
{
    public class ContactInfo
    {
        public object ID { get; set; }
        public object EmployeeID { get; set; }
        public byte ContactType { get; set; }
        public string ContactData { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}