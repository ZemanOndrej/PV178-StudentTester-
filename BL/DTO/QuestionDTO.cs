using System.Collections.Generic;
using DAL.Entities;

namespace BL.DTO
{
	public class QuestionDTO
	{
		public int Id { get; set; }

		public string Text { get; set; }
		public bool OneAnswer { get; set; }
		public List<AnswerDTO> Answers { get; set; }
		
		public int Points { get; set; }
		public ThematicAreaDTO ThematicArea { get; set; }

		public QuestionDTO()
		{

			
			Answers =new List<AnswerDTO>();
		}
	}
}