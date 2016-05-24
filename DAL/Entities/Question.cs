using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
	public class Question
	{
		public int Id { get; set; }
		[Required]
		public string Text { get; set; }
		public bool OneAnswer { get; set; }
		public List<Answer> Answers { get; set; }
		public int Points { get; set; }
		[Required]
		public ThematicArea ThematicArea { get; set; }
		

		public Question()
		{
			
			Answers = new List<Answer>();
		}
	}
}