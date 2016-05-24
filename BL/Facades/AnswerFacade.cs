using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace BL.Facades
{
	public class AnswerFacade
	{
		#region create
		public void CreateAnswer(AnswerDTO answer)
		{
			var newAnswer = Mapping.Mapper.Map<Answer>(answer);

			using (var context = new AppDbContext())
			{
//				context.Database.Log = Console.WriteLine;
				context.Answers.Add(newAnswer);
				context.SaveChanges();
			}
		}
		public void CreateManyAnswers(IEnumerable<AnswerDTO> answers)
		{
			foreach (var answer in answers)
			{
				CreateAnswer(answer);
			}
		}

		#endregion

		#region delete
				public void DeleteAnswer(AnswerDTO answer)
				{
					var newAnswer = Mapping.Mapper.Map<Answer>(answer);

					using (var context = new AppDbContext())
					{
//						context.Database.Log = Console.WriteLine;
						context.Entry(newAnswer).State = EntityState.Deleted;
						context.SaveChanges();
					};
				}

				public void DeleteAnswer(int id)
				{
					using (var context = new AppDbContext())
					{
//						context.Database.Log = Console.WriteLine;
						var Answer = context.Answers.Find(id);
						context.Answers.Remove(Answer);
						context.SaveChanges();
					};
				}
		#endregion

		#region get
				public AnswerDTO GetAnswerById(int id)
				{
					using (var context = new AppDbContext())
					{
//						context.Database.Log = Console.WriteLine;
						var answer = context.Answers.Include(a=>a.Question).ToList().Find(a=>a.Id==id);
						
				return Mapping.Mapper.Map<AnswerDTO>(answer);
					}
				}

				public List<AnswerDTO> GetAllAnswers()
				{
					using (var context = new AppDbContext())
					{

						var answers = context.Answers.Include(a => a.Question).ToList();
							
				
						
						return answers
							.Select(element => Mapping.Mapper.Map<AnswerDTO>(element))
							.ToList();
					}
				}

		#endregion

		#region update
		public void UpdateAnswer(AnswerDTO answer)
		{
			var newAnswer = Mapping.Mapper.Map<Answer>(answer);

			using (var context = new AppDbContext())
			{
				context.Entry(newAnswer).State = EntityState.Modified;
				context.SaveChanges();
			}
		}
#endregion

	}
}