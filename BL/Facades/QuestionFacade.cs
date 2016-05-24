using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace BL.Facades
{
	public class QuestionFacade
	{
		#region create
		public void CreateQuestion(QuestionDTO question, string thematicArea)
		{
			
			var newQuestion = Mapping.Mapper.Map<Question>(question);
			var found = false;
			ThematicArea area;

			using (var context = new AppDbContext())
			{
				//context.Database.Log = Console.WriteLine;
				var oldThematicArea = context.ThematicAreas
					.SingleOrDefault(c => c.Name.Equals(thematicArea));
				if (oldThematicArea != null)
				{
					area = oldThematicArea;
					found = true;
				}
				else
				{
					area = new ThematicArea() { Name = thematicArea };
					area.Questions.Add(newQuestion);
				}
				context.SaveChanges();
			}
			area.Questions.Add(newQuestion);
			newQuestion.ThematicArea = area;
			using (var context = new AppDbContext())
			{
				//context.Database.Log = Console.WriteLine;
				if (found)
				{
					context.Entry(area).State = EntityState.Modified;
				}
				context.Questions.Add(newQuestion);
				context.SaveChanges();
			}
		
		}

		public void CreateManyQuestions(IEnumerable<QuestionDTO> questions, string thematicArea)
		{
			foreach (var question in questions)
			{
				CreateQuestion(question, thematicArea);
			}
		}

		#endregion

		#region delete
		public void DeleteQuestion(QuestionDTO question)
		{
			var newQuestion = Mapping.Mapper.Map<Question>(question);

			using (var context = new AppDbContext())
			{
				//context.Database.Log = Console.WriteLine;
				context.ThematicAreas.Find(newQuestion.ThematicArea).Questions.Remove(newQuestion);

				context.Entry(newQuestion).State = EntityState.Deleted;
				context.SaveChanges();
			}
		}

		public void DeleteQuestion(int id)
		{
			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				var question = context.Questions.Find(id);
				

				context.ThematicAreas.Find(question.ThematicArea).Questions.Remove(question);

				context.Questions.Remove(question);



				context.SaveChanges();
			}
		}

		

		public void RemoveAllQuestions()
		{
			using (var context = new AppDbContext())
			{
				foreach (var q in context.Questions.ToList())
				{
					context.Questions.Remove(q);
				}

				context.SaveChanges();

			}
		}
		#endregion

		#region get



		public List<QuestionDTO> GetNumOfRandQuestionsFromThematicAreas(int num, List<ThematicAreaDTO> areasDto)
		{

			var areas = areasDto.Select(area => Mapping.Mapper.Map<ThematicArea>(area)).ToList();
			using (var context = new AppDbContext())
			{
				var questtions = new List<Question>();
				foreach (var area in areas)
				{
					questtions.AddRange(context.Questions
						.Where(q=>q.ThematicArea.Name.Equals(area.Name))
							.Include(q=>q.Answers));
				}



				return questtions.Select(q=> Mapping.Mapper.Map<QuestionDTO>(q)).Take(num).ToList();
			}
		}




		public QuestionDTO GetQuestionById(int id)
		{
			using (var context = new AppDbContext())
			{
				context.Database.Log = Console.WriteLine;
				var qs = context.Questions.Include(t=>t.ThematicArea).ToList();
				var question = qs.Find(q => q.Id == id);
				if(question == null)
				return null;

				context.Entry(question).Collection(c => c.Answers).Load();

				return Mapping.Mapper.Map<QuestionDTO>(question);
			}
		}
		public List<QuestionDTO> GetAllQuestions()
		{
			using (var context = new AppDbContext())
			{
				
				var questions = context.Questions.Include(t=>t.ThematicArea)
					.Include(t=>t.Answers).ToList();
				

				return questions
					.Select(element => Mapping.Mapper.Map<QuestionDTO>(element))
					.ToList();
			}
		}
		#endregion

		#region update
		public void UpdateQuestion(QuestionDTO question)
		{
			var newQuestion = Mapping.Mapper.Map<Question>(question);

			using (var context = new AppDbContext())
			{
				if (context.Entry(newQuestion).Entity.ThematicArea.Name == newQuestion.ThematicArea.Name)
				{
					context.Entry(newQuestion).State = EntityState.Modified;
					
				}
				else
				{
					var thematicArea = context.ThematicAreas
					.SingleOrDefault(c => c.Name.Equals(newQuestion.ThematicArea.Name));
					if (thematicArea != null)
					{
						if (!newQuestion.ThematicArea.Questions.Contains(newQuestion))
						{
							newQuestion.ThematicArea.Questions.Add(newQuestion);
						}
						context.ThematicAreas.Add(newQuestion.ThematicArea);
						
					}
					else
					{
						newQuestion.ThematicArea.Questions.Add(newQuestion);
						context.Entry(newQuestion.ThematicArea).State = EntityState.Modified;
						
					}
				}
				context.SaveChanges();
			}
		}
		#endregion
		
	}
}