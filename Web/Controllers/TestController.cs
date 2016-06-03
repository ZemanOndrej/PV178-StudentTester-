using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Microsoft.AspNet.Identity;
using Web.Models;

namespace Web.Controllers
{
	[Authorize ]
	public class TestController : Controller
	{
		TestTemplateFacade testFacade = new TestTemplateFacade();

		#region TestEdit
		[Authorize(Roles = "admin")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(TestTemplateDTO test)
		{
			try
			{

				int id2 = testFacade.CreateTestTemplate(test);

				return RedirectToAction("Edit", new { id = id2 });
			}
			catch
			{
				return View();
			}
		}

		[Authorize(Roles = "admin")]
		public ActionResult Edit(int id)
		{
			var test = testFacade.GetTestTemplateById(id);
			return View(new EditTestModel { test = test });
		}

		[HttpPost]
		public ActionResult Edit(int id, EditTestModel testEdit, string addArea, string save, string addGroup)
		{

			//			if (!ModelState.IsValid) return RedirectToAction("Index"); 
			try
			{

				if (!string.IsNullOrEmpty(save))
				{


					var originalTest = testFacade.GetTestTemplateById(id);
					originalTest.Date = testEdit.test.Date;
					originalTest.Name = testEdit.test.Name;
					originalTest.CompletionTime = testEdit.test.CompletionTime;
					originalTest.NumOfQuestions = testEdit.test.NumOfQuestions;

					testFacade.UpdateTestTemplate(originalTest);

					return RedirectToAction("Index");

				}

				if (!string.IsNullOrEmpty(addArea))
				{


					testFacade.AddTheme(id, testEdit.area.Name);

					return RedirectToAction("Edit", new { id });



				}
				if (!string.IsNullOrEmpty(addGroup))
				{

					testFacade.AddStudentGroup(id, testEdit.group);
					return RedirectToAction("Edit", new { id });

				}
			}
			catch
			{
				return RedirectToAction("Edit", new { id });
			}
			return RedirectToAction("Index");
		}
		[Authorize(Roles = "admin")]
		public ActionResult Delete(int id)
		{
			testFacade.DeleteTestTemplate(id);
			return RedirectToAction("Index");
		}
		[Authorize(Roles = "admin")]
		public ActionResult RemoveTheme(int themeId, int testId)
		{

			testFacade.RemoveThemArea(testId, themeId);
			return RedirectToAction("Edit", new { id = testId });
		}
		[Authorize(Roles = "admin")]
		public ActionResult RemoveStudentGroup(int studgrpid, int testid)
		{
			testFacade.RemoveStudentGroup(studgrpid, testid);
			return RedirectToAction("Edit", new { id = testid });
		}

		#endregion

		public ActionResult Index()
		{
			if (User.IsInRole("admin"))
			{
				return View(testFacade.GetAllTestTemplates());
			}
			if (User.IsInRole("student"))
			{
				var tests = testFacade.GetTestsForUser(int.Parse(User.Identity.GetUserId()));
				return View(tests);
			}
			return RedirectToAction("Index", "Home");
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
