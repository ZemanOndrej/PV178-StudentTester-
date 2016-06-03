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
		public int CreateTestTemplate(TestTemplateDTO testTemplate)
		{
			var newTestTemplate = Mapping.Mapper.Map<TestTemplate>(testTemplate);

			using (var context = new AppDbContext())
			{
				
				context.TestTemplates.Add(newTestTemplate);
				context.SaveChanges();
				return newTestTemplate.Id;
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
				context.Entry(testTemplate).Collection(c => c.StudentGroups).Load();
				return Mapping.Mapper.Map<TestTemplateDTO>(testTemplate);
			}
		}

		public List<TestTemplateDTO> GetAllTestTemplates()
		{
			using (var context = new AppDbContext())
			{
				var testTemplates = context.TestTemplates.Include(c=>c.ThematicAreas).Include(x=>x.StudentGroups).ToList();
				
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
				context.Entry(tt).Collection(c => c.StudentGroups).Load();

				return Mapping.Mapper.Map<TestTemplateDTO> (tt);
			}
			
		}

		public List<TestTemplateDTO> GetTestsForUser(int id)
		{
			var list = new List<TestTemplateDTO>();
			using (var context = new AppDbContext())
			{
				var user = context.Users.Include(s => s.StudentGroups).SingleOrDefault(s => s.Id == id);


				var groups = context.StudentGroups.Include(s => s.Tests);
				foreach (var group in user.StudentGroups)
				{
					var grp = groups.SingleOrDefault(g => g.Name.Equals(group.Name));


					list.AddRange(grp.Tests.Select(test => Mapping.Mapper.Map<TestTemplateDTO>(test)));

				}
				return list;
			}
		} 
		#endregion

		#region update

		public void RemoveThemArea(int testId, int themeId)
		{
			using (var context = new AppDbContext())
			{

				var tt = context.TestTemplates.Find(testId);
				context.Entry(tt).Collection(c => c.ThematicAreas).Load();
				context.Entry(tt).Collection(c=>c.StudentGroups).Load();
				


				tt.ThematicAreas.Remove(tt.ThematicAreas.FirstOrDefault(t => t.Id == themeId));

				
				context.Entry(tt).State = EntityState.Modified;
				


				context.SaveChanges();
			}
		}

		public void RemoveStudentGroup( int stdgrpId , int testId)
		{
			using (var context = new AppDbContext())
			{
				var tt = context.TestTemplates.Find(testId);
				context.Entry(tt).Collection(c => c.ThematicAreas).Load();
				context.Entry(tt).Collection(c => c.StudentGroups).Load();


				tt.StudentGroups.Remove(tt.StudentGroups.FirstOrDefault(t => t.Id == stdgrpId));


				context.Entry(tt).State = EntityState.Modified;



				context.SaveChanges();
			}
		}

		public void UpdateTestTemplate(TestTemplateDTO testTemplate)
		{
			var newTestTemplate = Mapping.Mapper.Map<TestTemplate>(testTemplate);
			using (var context = new AppDbContext())
			{
				
				context.Entry(newTestTemplate).State = EntityState.Modified;

				context.SaveChanges();
			}
			



		}


		public void AddTheme(int testId, string area)
		{

			var themFac = new ThematicAreaFacade();

			using (var context = new AppDbContext())
			{

				var tt = context.TestTemplates
					.Include(s => s.StudentGroups)
					.Include(s => s.ThematicAreas)
					.SingleOrDefault(s => s.Id==testId);


				themFac.CreateThematicArea(area);

				var theme = context.ThematicAreas.SingleOrDefault(s => s.Name.Equals(area));
				if (tt != null)
				{
					tt.ThematicAreas.Add(theme);
					context.Entry(tt).State = EntityState.Modified;
				}


				context.SaveChanges();
			}

		}


		public void AddStudentGroup(int testId, StudentGroupDTO group)
		{
			var groupFac = new StudentGroupFacade();
			using (var context = new AppDbContext())
			{
				var tt = context.TestTemplates
					.Include(s => s.StudentGroups).Include(s => s.ThematicAreas)
					.SingleOrDefault(s => s.Id == testId);


				groupFac.CreateStudentGroup(group);

				var grp = context.StudentGroups.SingleOrDefault(s => s.Name.Equals(group.Name));
				if (tt != null)
				{
					tt.StudentGroups.Add(grp);
					grp.Tests.Add(tt);
					context.Entry(grp).State = EntityState.Modified;
					context.Entry(tt).State = EntityState.Modified;
				}


				context.SaveChanges();
			}
			
			
		}
		#endregion

	}
}