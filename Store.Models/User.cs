using System;

namespace Store.Models
{
	public class User
	{
		public object ID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime CreateDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
