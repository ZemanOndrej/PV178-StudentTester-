using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL2try.IdentityEntities;

namespace DAL.Entities
{
	public class StudentGroup
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public List<AppUser> Students { get; set; }
		public int RegId { get; set; }
		public List<TestTemplate> Tests { get; set; } 
		
		public StudentGroup()
		{
			
			Tests= new List<TestTemplate>();
			Students = new List<AppUser>();
		}
	}
}