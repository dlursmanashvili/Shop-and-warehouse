using System;

namespace Store.Models
{
    public class Permission
    {
        public object ID { get; set; }
        public string PermissionName { get; set; }
        public short PermissionCode { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}