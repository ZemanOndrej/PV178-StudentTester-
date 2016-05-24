using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;

namespace Web.Controllers
{
	[Authorize]
	public class TestController : Controller
	{
		TestTemplateFacade testFacade = new TestTemplateFacade();
		// GET: Test
		public ActionResult Index()
		{
			
			var model = new TestModel() {Tests = testFacade.GetAllTestTemplates()};
			return View(model.Tests);
		}

		// GET: Test/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Test/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Test/Create
		[HttpPost]
		public ActionResult Create(TestTemplateDTO test)
		{
			try
			{
				testFacade.CreateTestTemplate(test);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Test/Edit/5
		public ActionResult Edit(int id)
		{
			var test = testFacade.GetTestTemplateById(id);
			return View(test);
		}

		// POST: Test/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, TestTemplateDTO test)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var originalTest = testFacade.GetTestTemplateById(id);
					originalTest.Date = test.Date;
					originalTest .Name = test.Name;
					originalTest.CompletionTime = test.CompletionTime;
					originalTest.NumOfQuestions = test.NumOfQuestions;

					testFacade.UpdateTestTemplate(originalTest);

					return RedirectToAction("Index");
				}
				return View(test);
			}
			catch
			{
				return View();
			}
		}

		// GET: Test/Delete/5
		public ActionResult Delete(int id)
		{
			testFacade.DeleteTestTemplate(id);
			return RedirectToAction("Index"); 
		}



		[HttpGet]
		public ActionResult TakeTest(int id)
		{

			var testTmp = testFacade.GetTestTemplateById(id);
			var qfac = new QuestionFacade();
			var model = new TestActiveModel
			{
				Questions = qfac.GetNumOfRandQuestionsFromThematicAreas(5, testTmp.ThematicAreas)
				,Test = testTmp
			};
			
			return View(model);
		}
		
	}
}
