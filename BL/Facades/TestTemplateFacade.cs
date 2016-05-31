using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace BL.Facades
{
	public class TestTemplateFacade
	{
		#region create
		public void CreateTestTemplate(TestTemplateDTO testTemplate)
		{
			var newTestTemplate = Mapping.Mapper.Map<TestTemplate>(testTemplate);

			using (var context = new AppDbContext())
			{
				if (context.TestTemplates.FirstOrDefault(s => s.Name.Equals(testTemplate.Name)) != null) return;

				context.TestTemplates.Add(newTestTemplate);
				context.SaveChanges();
			}
		}

		public void CreateManyTestTemplates(IEnumerable<TestTemplateDTO> testTemplates)
		{
			foreach (var testTemplate in testTemplates)
			{
				CreateTestTemplate(testTemplate);
			}
		}

		#endregion


		#region delete
		public void DeleteTestTemplate(TestTemplateDTO testTemplate)
		{
			var newTestTemplate = Mapping.Mapper.Map<TestTemplate>(testTemplate);

			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				context.Entry(newTestTemplate).State = EntityState.Deleted;
				context.SaveChanges();
			};
		}

		public void DeleteTestTemplate(int id)
		{
			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				var testTemplate = context.TestTemplates.Find(id);
				context.TestTemplates.Remove(testTemplate);
				context.SaveChanges();
			};
		}


		public void RemoveAllTestTemplates()
		{
			using (var context = new AppDbContext())
			{
				foreach (var q in context.TestTemplates.ToList())
				{
					context.TestTemplates.Remove(q);
				}

				context.SaveChanges();

			}
		}

		#endregion

		#region get
		public TestTemplateDTO GetTestTemplateById(int id)
		{
			using (var context = new AppDbContext())
			{
				
				var testTemplate = context.TestTemplates.Find(id);
				context.Entry(testTemplate).Collection(c => c.ThematicAreas).Load();
				return Mapping.Mapper.Map<TestTemplateDTO>(testTemplate);
			}
		}

		public List<TestTemplateDTO> GetAllTestTemplates()
		{
			using (var context = new AppDbContext())
			{
				var testTemplates = context.TestTemplates.Include(c=>c.ThematicAreas).ToList();
				
				return testTemplates
					.Select(element => Mapping.Mapper.Map<TestTemplateDTO>(element))
					.ToList();
			}
		}

		public TestTemplateDTO GetTemplateByName(string name)
		{
			using (var context = new AppDbContext())
			{
				var tt = (context.TestTemplates.FirstOrDefault(s => s.Name.Equals(name)));
				context.Entry(tt).Collection(c => c.ThematicAreas).Load();
				
				return Mapping.Mapper.Map<TestTemplateDTO> (tt);
			}
			
		}


		#endregion

		#region update
		public void UpdateTestTemplate(TestTemplateDTO testTemplate)
		{
			var newTestTemplate = Mapping.Mapper.Map<TestTemplate>(testTemplate);

			
			
			using (var context = new AppDbContext())
			{
				
				context.Entry(newTestTemplate).State = EntityState.Modified;

				context.SaveChanges();
			}
			



		}


		public void UpdateTestTemplateTheme(TestTemplateDTO testTemplate, string area)
		{
			

		
			using (var context = new AppDbContext())
			{

				var tt = Mapping.Mapper.Map<TestTemplate>(GetTemplateByName(testTemplate.Name));




				var themFac = new ThematicAreaFacade();
				themFac.CreateThematicArea(area);
				themFac.GetThematicAreaByName(area).Tests.Add(testTemplate);

				tt.ThematicAreas.Add(Mapping.Mapper.Map<ThematicArea>(themFac.GetThematicAreaByName(area)));
				

				context.Entry(tt).State = EntityState.Modified;



				context.SaveChanges();
			}



			
		}
		#endregion

	}
}