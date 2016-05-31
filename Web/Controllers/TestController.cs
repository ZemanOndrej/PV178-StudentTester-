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
			
			
			return View(testFacade.GetAllTestTemplates());
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
			return View(new EditTestModel { test = test });
		}

		// POST: Test/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, EditTestModel testEdit, string add, string save)
		{
			
			if (!ModelState.IsValid) return View(testEdit);

			
			if (!string.IsNullOrEmpty(save))
			{
				try
				{
					
					var originalTest = testFacade.GetTestTemplateById(id);
					originalTest.Date = testEdit.test.Date;
					originalTest.Name = testEdit.test.Name;
					originalTest.CompletionTime = testEdit.test.CompletionTime;
					originalTest.NumOfQuestions = testEdit.test.NumOfQuestions;

					testFacade.UpdateTestTemplate(originalTest);

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
					
//					var originalTest = testFacade.GetTestTemplateById(id);
					

					testFacade.UpdateTestTemplateTheme(testEdit.test,testEdit.area.Name);

					return RedirectToAction("Edit",id);
					

				}
				catch
				{
					return View();
				}
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


		[HttpPost]
		public ActionResult AddThematicArea(int id,  EditTestModel testEdit)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var originalTest = testFacade.GetTestTemplateById(id);

					originalTest.ThematicAreas.Add(testEdit.area);

					testFacade.UpdateTestTemplate(originalTest);

					return RedirectToAction("Edit");
				}

			}
			catch
			{
				
			}
			return Edit(id);
		}
	}
}
