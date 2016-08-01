using System;

namespace DAL.Entities
{
	public class Result
	{
		public int Id { get; set; }
		public int TestTemplateId { get; set; }
		public string TestTemplateName { get; set; }
		public int UserId { get; set; }
		public string ResultString { get; set; }
		public double ResultPoints { get; set; }
		public DateTime Time { get; set; }

		public Result()
		{
		}
	}
}