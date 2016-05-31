using System;
using System.Security.Claims;
using BL.DTO;
using BL.Identity;
using DAL;
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
			if (user.Code == null) return;
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
	}
}