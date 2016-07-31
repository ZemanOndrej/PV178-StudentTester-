using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace BL.Facades
{
	public class ResultFacade
	{
		public void CreateResult(ResultDTO resultdto )
		{
			var result = Mapping.Mapper.Map<Result>(resultdto);
			using (var context = new AppDbContext())
			{
				context.Results.Add(result);
				context.SaveChanges();
			}
		}
	}
}