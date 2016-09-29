using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using BL.DTO;
using BL.Identity;
using DAL;
using DAL.Entities;
using DAL2try.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace BL.Facades
{
	public class UserFacade
	{
		private const string Code = "Kappa";

		public void Register(UserDTO user)
		{
			var userManager = new AppUserManager(new AppUserStore(new AppDbContext()));

			var appUser = Mapping.Mapper.Map<AppUser>(user);
			
				
			userManager.Create(appUser, user.Password);
			var ourUser = userManager.FindByName(appUser.UserName);
			using (var context = new AppDbContext())
			{
				var userDb = context.Users.Include(s=>s.StudentGroups).SingleOrDefault(u => u.Id == ourUser.Id);
				var gr = context.StudentGroups.SingleOrDefault(s => s.Name.Equals("AllStudents"));
				userDb.StudentGroups.Add(gr);
				context.SaveChanges();

			}
			var roleManager = new AppRoleManager(new AppRoleStore(new AppDbContext()));

			if (!roleManager.RoleExists("admin"))
			{
				Console.WriteLine("admin role added");
				roleManager.Create(new AppRole { Name = "admin" });
			}
			if (!roleManager.RoleExists("student"))
			{
				Console.WriteLine("student role added");

				roleManager.Create(new AppRole { Name = "student" });
			}



			if (user.Code == null)
			{
				userManager.AddToRole(ourUser.Id, "student");
				return;
			}
			userManager.AddToRole(ourUser.Id, user.Code.Equals(Code) ? "admin" : "student");
		}

		public ClaimsIdentity Login(string email, string password)
		{
			var userManager = new AppUserManager(new AppUserStore(new AppDbContext()));
			try
			{
				var wantedUser = userManager.FindByEmail(email);

				if (wantedUser == null)
				{
					return null;
				}

				var user = userManager.Find(wantedUser.UserName, password);

				return user == null ? null : userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
			}
			catch 
			{
				return null;

			}
			
		}

		public IEnumerable<UserDTO> GetAllUsers()
		{
			using (var context = new AppDbContext())
			{
				var students = context
					.Users
					.Include(s => s.StudentGroups)
					.ToList();
					
				return students
					.Select(element => Mapping.Mapper.Map<UserDTO>(element))
					.ToList();

			}
		}

		public UserDTO GetUserById(int id)
		{
			using (var context = new AppDbContext())
			{
				return Mapping.Mapper.Map<UserDTO>(context.Users.Include(s => s.StudentGroups).SingleOrDefault(s => s.Id == id));
			}
		}

		public void RemoveUser(int id)
		{
			using (var context = new AppDbContext())
			{
				AppUser user;
				user = context.Users.Include(s=>s.StudentGroups).SingleOrDefault(s => s.Id == id);
				context.Users.Remove(user);
				context.SaveChanges();
			}
			
		}

		public void AddStudentGroup(int id, StudentGroupDTO group)
		{
			var groupFac = new StudentGroupFacade();
			using (var context = new AppDbContext())
			{
				var student = context.Users
					.Include(s => s.StudentGroups)
					.SingleOrDefault(s => s.Id == id);
				groupFac.CreateStudentGroup(group);


				StudentGroup grp;
				if (string.IsNullOrEmpty(group.Name))
				{
					grp = context.StudentGroups.SingleOrDefault(g => g.RegId == group.RegId);
				}
				else
				{
					grp = context.StudentGroups.SingleOrDefault(s => s.Name.Equals(group.Name));

				}

				if (student != null)
				{
					student.StudentGroups.Add(grp);
					grp.Students.Add(student);
					context.Entry(grp).State = EntityState.Modified;
					context.Entry(student).State = EntityState.Modified;
				}


				context.SaveChanges();



			}
		}

		public void UpdateUser(UserDTO user)
		{


			var userManager = new AppUserManager(new AppUserStore(new AppDbContext()));
			var updatedUser = Mapping.Mapper.Map<AppUser>(user);
			using (var context = new AppDbContext())
			{
				var oldUser = userManager.FindByEmail(user.Email);
				if (  string.IsNullOrEmpty(updatedUser.PasswordHash))
				{
					updatedUser.PasswordHash = oldUser.PasswordHash;
				}
				if (string.IsNullOrEmpty(updatedUser.Code))
				{
					updatedUser.Code = oldUser.Code;
				}
				if (string.IsNullOrEmpty(updatedUser.SecurityStamp))
				{
					updatedUser.SecurityStamp = oldUser.SecurityStamp;
				}
				


				context.Entry(updatedUser).State = EntityState.Modified;

				context.SaveChanges();
			}
		}

		public void RemoveStudentGroup(int studgrpId, int userId)
		{
			using (var context = new AppDbContext())
			{
				var user = context.Users.Find(userId);
				context.Entry(user).Collection(c => c.StudentGroups).Load();

				user.StudentGroups.Remove(user.StudentGroups.FirstOrDefault(t => t.Id == studgrpId));

				context.Entry(user).State = EntityState.Modified;


				context.SaveChanges();
			}
		}
	}
}