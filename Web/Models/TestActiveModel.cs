using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;

namespace Web.Models
{
	public class TestActiveModel
	{
		
		public string Name { get; set; }
		public int NumOfQuestions { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan CompletionTime { get; set; }
		public int Id { get; set; }
		public List<QuestionDTO> Questions { get; set; }
		public CheckboxModel[] Answers { get; set; }
	}

	public class CheckboxModel
	{
		public bool Selected { get; set; }
		public int Id { get; set; }
	}


}
