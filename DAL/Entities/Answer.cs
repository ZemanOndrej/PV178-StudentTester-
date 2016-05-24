using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
	public class Answer
	{
		public int Id { get; set; }
		
		public Question Question { get; set; }
		public string Text { get; set; }
		
		public string Description { get; set; }
		public bool Correct { get; set; }


		public Answer()
		{
			
		}
	}
}