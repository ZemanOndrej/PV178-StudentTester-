using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
	public class AnswerDTO
	{
		public int Id { get; set; }
		
		public QuestionDTO Question { get; set; }
		public string Text { get; set; }

		public string Description { get; set; }
		public bool Correct { get; set; }

		public AnswerDTO()
		{
			
		}
	}
}