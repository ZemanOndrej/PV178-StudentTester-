using System;
using System.Collections.Generic;
using DAL.Entities;

namespace BL.DTO
{
	public class TestTemplateDTO
	{
		

		public int Id { get; set; }

		public string Name { get; set; }

		public int NumOfQuestions { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan CompletionTime { get; set; }

		public List<ThematicAreaDTO> ThematicAreas { get; set; }
		public List<StudentGroupDTO> StudentGroups { get; set; } 

		public TestTemplateDTO()
		{
			CompletionTime=new TimeSpan();
			ThematicAreas = new List<ThematicAreaDTO>();
			StudentGroups= new List<StudentGroupDTO>();
			Date= DateTime.MinValue;
		}
	}
}