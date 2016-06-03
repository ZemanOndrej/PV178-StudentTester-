using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace BL.Facades
{
	public class StudentGroupFacade
	{
		#region create

		public void CreateStudentGroup(StudentGroupDTO studentGroup)
		{
			var newStudentGroup = Mapping.Mapper.Map<StudentGroup>(studentGroup);
			if (string.IsNullOrEmpty(studentGroup.Name)) return;
			using (var context = new AppDbContext())
			{
				if (context.StudentGroups.SingleOrDefault(s => s.Name.Equals(studentGroup.Name)) != null) return;
				if (context.StudentGroups.SingleOrDefault(s => s.RegId==studentGroup.RegId) != null) return;
				context.StudentGroups.Add(newStudentGroup);
				context.SaveChanges();
			}
		}

		public void CreateManyStudentGroups(IEnumerable<StudentGroupDTO> studentGroups)
		{
			foreach (var studentGroup in studentGroups)
			{
				CreateStudentGroup(studentGroup);
			}
		}

		#endregion

		#region delete
		public void DeleteStudentGroup(StudentGroupDTO studentGroup)
		{
			var newStudentGroup = Mapping.Mapper.Map<StudentGroup>(studentGroup);

			using (var context = new AppDbContext())
			{
				
				context.Entry(newStudentGroup).State = EntityState.Deleted;
				context.SaveChanges();
			};
		}

		public void DeleteStudentGroup(int id)
		{
			using (var context = new AppDbContext())
			{
				
				
				var studentGroup = context.StudentGroups.Find(id);
				context.Entry(studentGroup).Collection(c => c.Students).Load();
				context.Entry(studentGroup).Collection(c => c.Tests).Load();


				context.Entry(studentGroup).State = EntityState.Deleted;
				context.SaveChanges();
			};
		}
		#endregion

		#region get
		public StudentGroupDTO GetStudentGroupById(int id)
		{
			using (var context = new AppDbContext())
			{
				
				var studentGroup = context.StudentGroups.Find(id);
				context.Entry(studentGroup).Collection(c => c.Students).Load();
				
				context.Entry(studentGroup).Collection(c => c.Tests).Load();
				return Mapping.Mapper.Map<StudentGroupDTO>(studentGroup);
			}
		}

		public List<StudentGroupDTO> GetAllStudentGroups()
		{
			using (var context = new AppDbContext())
			{
				var studentGroups = context
					.StudentGroups
					.Include(c=>c.Students)
					
					.Include(c=>c.Tests)
					.ToList();
				return studentGroups
					.Select(element => Mapping.Mapper.Map<StudentGroupDTO>(element))
					.ToList();
			}
		}
		#endregion

		#region update
		public void UpdateStudentGroup(StudentGroupDTO studentGroup)
		{
			var newStudentGroup = Mapping.Mapper.Map<StudentGroup>(studentGroup);

			using (var context = new AppDbContext())
			{
				context.Entry(newStudentGroup).State = EntityState.Modified;
				context.SaveChanges();
			}
		}

		#endregion
		
	}
}