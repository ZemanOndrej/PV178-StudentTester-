using DAL2try.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace BL.Identity
{
	public class AppUserManager : UserManager<AppUser, int>
	{
		public AppUserManager(IUserStore<AppUser, int> store) : base(store)
		{
		}
	}
}