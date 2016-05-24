using System.Collections.Generic;
using DAL.Entities;

namespace BL.DTO
{
	public class UserDTO
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public List<StudentGroup> StudentGroups { get; set; }
	}
}