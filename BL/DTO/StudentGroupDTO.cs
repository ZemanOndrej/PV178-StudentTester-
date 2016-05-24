using System.Collections.Generic;
using DAL.Entities;

namespace BL.DTO
{
	public class StudentGroupDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public List<UserDTO> Students { get; set; }
		public int RegId { get; set; }
		public List<TestTemplateDTO> Tests { get; set; }
		

		public StudentGroupDTO()
		{
			
			Students = new List<UserDTO>();
			Tests = new List<TestTemplateDTO>();
		}
	}
}