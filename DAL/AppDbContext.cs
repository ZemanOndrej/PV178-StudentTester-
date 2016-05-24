using System;
using System.Data.Entity;
using DAL.Entities;
using DAL2try.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
	public class AppDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
	{
		
		
		
		public DbSet<ThematicArea> ThematicAreas { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<StudentGroup> StudentGroups { get; set; }
		public DbSet<TestTemplate> TestTemplates { get; set; }

		public AppDbContext() : base("AppDB2")
		{
		}
//		protected override void OnModelCreating(DbModelBuilder modelBuilder)
//		{
//			base.OnModelCreating(modelBuilder);
//			modelBuilder.Entity<Student>()
//				.MapToStoredProcedures(s => s
//					.Update(u => u.HasName("modify_customer"))
//					.Delete(d => d.HasName("delete_customer"))
//					.Insert(i => i.HasName("insert_customer"))
//				);
//		}

	}
}