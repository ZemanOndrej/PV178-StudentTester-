using System.Collections.Generic;
using System.Data.Entity;
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

		public List<ResultDTO> GetAllResultsFromUser(int userId)
		{
			using (var context = new AppDbContext())
			{
				return context.Results
					.ToList()
					.Where(r=>r.UserId==userId)
					.Select(element => Mapping.Mapper.Map<ResultDTO>(element))
					.ToList();
			}
		}
		public List<ResultDTO> GetAllResults()
		{
			using (var context = new AppDbContext())
			{
				return context.Results
					.ToList()
					.Select(result => Mapping.Mapper.Map<ResultDTO>(result))
					.ToList();
			}
		}

		public void RemoveResult(ResultDTO resultDto)
		{
			var result = Mapping.Mapper.Map<Result>(resultDto);

			using (var context = new AppDbContext())
			{
			
				context.Entry(result).State = EntityState.Deleted;
				context.SaveChanges();
			}
		}

		public ResultDTO GetResultWithId(int id)
		{
			using (var context = new AppDbContext())
			{
				return Mapping.Mapper.Map<ResultDTO>(context.Results.FirstOrDefault(d => d.Id == id));
			}
		}

	}
}