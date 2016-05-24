using System.Data.Entity;
using DAL2try.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL.Identity
{
	public class AppUserStore : UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
	{
		public AppUserStore(DbContext context) : base(context)
		{

		}
	}
}