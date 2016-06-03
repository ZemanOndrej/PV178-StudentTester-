using BL.DTO;

namespace Web.Models
{
	public class EditTestModel
	{
		public TestTemplateDTO test { get; set; }
		public ThematicAreaDTO area { get; set; }
		public StudentGroupDTO group { get; set; }
	}
}