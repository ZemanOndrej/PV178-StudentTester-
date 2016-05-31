using System;
using System.Collections.Generic;

namespace DAL.Entities
{
	public class TestTemplate
	{
		
		public int Id { get; set; }
		
		public string Name { get; set; }

		public int NumOfQuestions { get; set; }
		
		public string Date { get; set; }

		public TimeSpan CompletionTime { get; set; }
		
		public List<ThematicArea> ThematicAreas { get; set; }

		public TestTemplate()
		{
			
			ThematicAreas= new List<ThematicArea>();
		}


	}
}