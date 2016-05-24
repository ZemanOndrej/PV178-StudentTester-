using DAL2try.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace BL.Identity
{
	public class AppRoleManager : RoleManager<AppRole, int>
	{
		public AppRoleManager(IRoleStore<AppRole, int> store) : base(store)
		{
			
		}
	}
}