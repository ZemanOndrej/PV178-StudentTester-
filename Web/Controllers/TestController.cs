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
		
		public ActionResult Index()
		{
			return View(testFacade.GetAllTestTemplates());
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(TestTemplateDTO test)
		{
			try
			{
				
				int id2 =testFacade.CreateTestTemplate(test);

				return RedirectToAction("Edit",new { id=id2});
			}
			catch
			{
				return View();
			}
		}
		
		public ActionResult Edit(int id)
		{
			var test = testFacade.GetTestTemplateById(id);
			return View(new EditTestModel { test = test });
		}

		
		[HttpPost]
		public ActionResult Edit(int idn, EditTestModel testEdit, string add, string save)
		{
			
			if (!ModelState.IsValid) return View(testEdit);

			
			if (!string.IsNullOrEmpty(save))
			{
				try
				{
					
					var originalTest = testFacade.GetTestTemplateById(idn);
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
					
					testFacade.UpdateTestTemplateTheme(testEdit.test,testEdit.area.Name);

					return RedirectToAction("Edit",new { id=idn});

				}
				catch
				{
					return RedirectToAction("Edit", new {id=idn});
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

	}
}
