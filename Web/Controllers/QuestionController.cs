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
	public class QuestionController : Controller
	{

		private QuestionFacade questionFacade = new QuestionFacade();
		// GET: Question
		public ActionResult Index()
		{
			 
			return View(questionFacade.GetAllQuestions());
		}

		// GET: Question/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Question/Create
		public ActionResult Create()
		{
			return View(
				new QuestionModel
				{
					Question = new QuestionDTO { Answers = new List<AnswerDTO>() }
				}
				
				
				);
		}

		// POST: Question/Create
		[HttpPost]
		public ActionResult Create(QuestionModel model)
		{
			try
			{
				
				questionFacade.CreateQuestion(model.Question,model.Area);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Question/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Question/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Question/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Question/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		public ActionResult AddAnswer()
		{
			throw new NotImplementedException();
		}
	}
}
