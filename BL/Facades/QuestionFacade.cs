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
		public int CreateQuestion(QuestionDTO question, string thematicArea)
		{
			
			var newQuestion = Mapping.Mapper.Map<Question>(question);


			int id;
			using (var context = new AppDbContext())
			{
				
				var tA = context.ThematicAreas.SingleOrDefault(c => c.Name.Equals(thematicArea));
				if (tA != null)
				{
					tA.Questions.Add(newQuestion);
					newQuestion.ThematicArea = tA;
					context.Entry(tA).State = EntityState.Modified;
					context.SaveChanges();
				}
				else
				{
					var them = new ThematicArea { Name = thematicArea };

					them.Questions.Add(newQuestion);
					context.ThematicAreas.Add(them);

					newQuestion.ThematicArea = context.ThematicAreas.SingleOrDefault(t => t.Name.Equals(thematicArea));

					context.Questions.Add(newQuestion);
					
					context.SaveChanges();
					
				}

				id = newQuestion.Id;
			}

			
			
	

			
			
			return id;
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
				var quest = context.Questions
					.Include(t => t.ThematicArea).Include(t => t.Answers)
					.SingleOrDefault(q => q.Id == id);

				
				context.Answers.RemoveRange(quest.Answers);
				
				context.Questions.Remove(quest);
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
			using (var context = new AppDbContext())
			{
				var areas = areasDto.Select(dto => context.ThematicAreas
						.Include(t => t.Questions)
						.SingleOrDefault(a => dto.Name.Equals(a.Name)))
					.ToList();


				var questions = new List<Question>();

				foreach (var area in areas)
				{
					questions.AddRange(context.Questions
						.Where(q => q.ThematicArea.Name.Equals(area.Name))
							.Include(q => q.Answers));
				}

				var rng = new Random();
				int n = questions.Count;
				while (n > 1)
				{
					n--;
					int k = rng.Next(n + 1);
					var value = questions[k];
					questions[k] = questions[n];
					questions[n] = value;
				}




				return questions.Select(q=> Mapping.Mapper.Map<QuestionDTO>(q)).Take(num).ToList();
			}
		}




		public QuestionDTO GetQuestionById(int id)
		{
			using (var context = new AppDbContext())
			{
				
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

		public void AddAnswerToQuestion(QuestionDTO question, AnswerDTO answer)
		{
			
			var answerFac = new AnswerFacade();

			using (var context = new AppDbContext())
			{

				var quest = context.Questions
					.Include(t => t.ThematicArea).Include(t=>t.Answers)
					.SingleOrDefault(q=>q.Id==question.Id);
				

				if (quest == null)
					return ;
				context.Entry(quest).Collection(c => c.Answers).Load();


				
				int id =answerFac.CreateAnswer(answer);
				

				

				var option = context.Answers.Find(id);

				quest.Answers.Add(option);
				context.Entry(quest).State = EntityState.Modified;
				


				context.SaveChanges();
			}
		}

		public void RemoveAnswer(int ansId, int questId)
		{
			
			using (var context = new AppDbContext())
			{

				var quest = context.Questions
					.Include(t => t.ThematicArea).Include(t => t.Answers)
					.SingleOrDefault(q => q.Id == questId);


				quest.Answers.Remove(quest.Answers.SingleOrDefault(t => t.Id == ansId));
				context.Answers.Remove(context.Answers.SingleOrDefault(s=>s.Id == ansId));


				context.Entry(quest).State = EntityState.Modified;



				context.SaveChanges();
			}
		}
		#endregion
		
	}
}