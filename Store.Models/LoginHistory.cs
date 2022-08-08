using System;

namespace Store.Models
{
    public class LoginHistory
    {
        public object ID { get; set; }
        public object UserID { get; set; }
        public string Username { get; set; }
        public DateTime LoginDate { get; set; }
        public bool IsSuccessful { get; set; }
    }
}