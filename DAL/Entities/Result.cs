namespace DAL.Entities
{
	public class Result
	{
		public int Id { get; set; }
		public int TestTemplateId { get; set; }
		public int AppUserId { get; set; }
		public string ResultString { get; set; }

		public Result()
		{
		}
	}
}