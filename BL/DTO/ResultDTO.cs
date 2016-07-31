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
		public int TestTemplateDTOId { get; set; }
		public int UserDTOId { get; set; }
		public string ResultString { get; set; }

		public ResultDTO()
		{
		}
	}
}
