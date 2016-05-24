using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace BL.Facades
{
	public class ThematicAreaFacade
	{
		#region create
		public void CreateThematicArea(string thematicArea)
		{
			var newThematicArea = new ThematicArea() {Name = thematicArea};

			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				if (context.ThematicAreas.FirstOrDefault(t => t.Name.Equals(thematicArea)) == null)
				{
					context.ThematicAreas.Add(newThematicArea);
				}
				
				context.SaveChanges();
			}
		}
		public void CreateManyThematicAreas(IEnumerable<string> thematicAreas)
		{
			foreach (var thematicArea in thematicAreas)
			{
				CreateThematicArea(thematicArea);
			}
		}
#endregion

		#region delete
		public void DeleteThematicArea(ThematicAreaDTO thematicArea)
		{
			var newThematicArea = Mapping.Mapper.Map<ThematicArea>(thematicArea);

			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				context.Entry(newThematicArea).State = EntityState.Deleted;
				context.SaveChanges();
			};
		}

		public void DeleteThematicArea(int id)
		{
			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				var thematicArea = context.ThematicAreas.Find(id);
				context.ThematicAreas.Remove(thematicArea);
				context.SaveChanges();
			}
		}

		public void DeleteThematicArea(string name)
		{
			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				var thematicArea = context.ThematicAreas.SingleOrDefault(t=>t.Name.Equals(name));
				context.ThematicAreas.Remove(thematicArea);
				context.SaveChanges();
			}
		}

		public void RemoveAllThematicAreas()
		{
			using (var context = new AppDbContext())
			{
				foreach (var q in context.ThematicAreas.ToList())
				{
					context.ThematicAreas.Remove(q);
				}

				context.SaveChanges();

			}
		}
		#endregion

		#region get
		public ThematicAreaDTO GetThematicAreaById(int id)
		{
			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				var thematicArea = context.ThematicAreas.Find(id);
				context.Entry(thematicArea).Collection(c => c.Questions).Load();

				return Mapping.Mapper.Map<ThematicAreaDTO>(thematicArea);
			}
		}
		public ThematicAreaDTO GetThematicAreaByName(string name)
		{
			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				var thematicArea = context.ThematicAreas.SingleOrDefault(t => t.Name.Equals(name));
				context.Entry(thematicArea).Collection(c => c.Questions).Load();
				return Mapping.Mapper.Map<ThematicAreaDTO>(thematicArea);
			}
		}

		public List<ThematicAreaDTO> GetAllThematicAreas()
		{
			using (var context = new AppDbContext())
			{
				var thematicAreas = context.ThematicAreas.Include(c=>c.Questions).ToList();
				
				
				return thematicAreas
					.Select(element => Mapping.Mapper.Map<ThematicAreaDTO>(element))
					.ToList();
			}
		}
		#endregion

		#region update
		/*public void UpdateThematicArea(ThematicAreaDTO thematicArea)
		{
			var newThematicArea = Mapping.Mapper.Map<ThematicArea>(thematicArea);

			using (var context = new AppDbContext())
			{
				context.Entry(newThematicArea).State = EntityState.Modified;
				context.SaveChanges();
			}
		}*/

#endregion

	}
}