using System.CodeDom;
using System.Collections.Generic;

namespace DAL.Entities
{
	public class ThematicArea
	{
		public int Id { get; set; }

		public string Name { get; set; } 

		public List<Question> Questions { get; set; }

		public List<TestTemplate> Tests { get; set; } 

		public ThematicArea()
		{
			Questions=new List<Question>();
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