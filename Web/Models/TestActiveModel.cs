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
		public TestTemplateDTO Test { get; set; }
		public List<QuestionDTO> Questions { get; set; } 
	}
}
