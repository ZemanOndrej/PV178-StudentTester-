using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
	public class ResultDTO
	{
		public int Id { get; set; }
		public int TestTemplateId { get; set; }
		public string TestTemplateName { get; set; }
		public int UserId { get; set; }
		public string ResultString { get; set; }
		public double ResultPoints { get; set; }
		public DateTime Time { get; set; }

		public ResultDTO()
		{
		}
	}
}
