using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL2try.IdentityEntities
{
	public class AppUserRole : IdentityUserRole<int>
	{

	}

	public class AppRole : IdentityRole<int, AppUserRole>
	{

	}

	public class AppUserClaim : IdentityUserClaim<int>
	{

	}
	public class AppUserLogin : IdentityUserLogin<int>
	{
	}
	public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
	{

		
		public string Name { get; set; }
		public string Surname { get; set; }
		public List<StudentGroup> StudentGroups { get; set; }
		public string Code { get; set; }

		public AppUser()
		{
			
			StudentGroups = new List<StudentGroup>();
		}
	}
}
