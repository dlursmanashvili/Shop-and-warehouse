using System;

namespace Store.Models
{
    public class Transaction
    {
        public object ID { get; set; }
        public object UserID { get; set; }
        public short TransactionType { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
