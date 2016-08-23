using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;

namespace Web.Models
{
	public class ResultModel
	{
		public string Name { get; set; }
		public int NumOfQuestions { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan CompletionTime { get; set; }
		public int Id { get; set; }
		public List<QuestionDTO> Questions { get; set; }
		public CheckboxModel[] Answers { get; set; }
		public List<bool> ResultBools { get; set; }
		public double Score { get; set; }
	}
}
