using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
	public class ThematicArea
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } 

		public List<Question> Questions { get; set; }

		public List<TestTemplate> Tests { get; set; } 

		public ThematicArea()
		{
			Questions=new List<Question>();
			Tests = new List<TestTemplate>();
		}

		public override bool Equals(object obj)
		{
			var dto = obj as ThematicArea;
			return dto != null && dto.Name.Equals(Name);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}