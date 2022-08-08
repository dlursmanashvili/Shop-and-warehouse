using System;

namespace Store.Models
{
    public class Role
    {
        public object ID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}