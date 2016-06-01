using BL.DTO;

namespace Web.Models
{
	public class EditQuestionModel
	{
		public QuestionDTO Question { get; set; }
		public AnswerDTO Answer { get; set; }
	}
}