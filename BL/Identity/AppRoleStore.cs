using System.Data.Entity;
using DAL2try.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL.Identity
{
	public class AppRoleStore : RoleStore<AppRole, int, AppUserRole>
	{
		public AppRoleStore(DbContext context) : base(context)
		{
		}
	}
}