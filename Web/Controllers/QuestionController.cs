using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;

namespace Web.Controllers
{
	[Authorize]
	public class QuestionController : Controller
	{

		private QuestionFacade questionFacade = new QuestionFacade();
		
		public ActionResult Index()
		{
			 
			return View(questionFacade.GetAllQuestions());
		}


		
		public ActionResult Create()
		{
			return View(
				new QuestionModel
				{
					Question = new QuestionDTO { Answers = new List<AnswerDTO>() }
					
				}
				
				
				);
		}

		
		[HttpPost]
		public ActionResult Create(QuestionModel model)
		{
			try
			{
				
				int id2=questionFacade.CreateQuestion(model.Question,model.Area);
				return RedirectToAction("Edit",new {id=id2});
			}
			catch
			{
				return View();
			}
		}

		
		public ActionResult Edit(int id)
		{
			return View(new EditQuestionModel
			{
				Question = questionFacade.GetQuestionById(id)
				
			});
		}

		
		[HttpPost]
		public ActionResult Edit(int id, EditQuestionModel qEdit, string add, string save)
		{
			if (!ModelState.IsValid) return View(qEdit);
			qEdit.Question.Id = id;

			if (!string.IsNullOrEmpty(save))
			{
				try
				{

					var originalQ = questionFacade.GetQuestionById(id);
					originalQ.ThematicArea = qEdit.Question.ThematicArea;
					originalQ.OneAnswer = qEdit.Question.OneAnswer;
					originalQ.Text = qEdit.Question.Text;
					originalQ.Points = qEdit.Question.Points;

					questionFacade.UpdateQuestion(originalQ);

					return RedirectToAction("Index");
				}
				catch
				{
					return View();
				}
			}
			if (string.IsNullOrEmpty(add)) return View();
			{
				try
				{
					
					questionFacade.AddAnswerToQuestion(qEdit.Question,qEdit.Answer);

					return RedirectToAction("Edit", new { id});

				}
				catch
				{
					return RedirectToAction("Edit", new { id });

				}
			}
		}


		public ActionResult Delete(int id)
		{
			questionFacade.DeleteQuestion(id);
			return RedirectToAction("Index");
		}

		
		public ActionResult DeleteAnswer(int ansId,int questId)
		{
			questionFacade.RemoveAnswer(ansId,questId);
			return RedirectToAction("Edit", new { id = questId });
		}


	}
}
