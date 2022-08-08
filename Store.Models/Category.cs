using System;

namespace Store.Models
{
    public class Category
    {
        public object ID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public object ParentID { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}