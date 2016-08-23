using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		ResultFacade resultFacade = new ResultFacade();
		QuestionFacade questionFacade = new QuestionFacade();

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

		
		
		public ActionResult TakeTest(int id)
		{

			var testTmp = testFacade.GetTestTemplateById(id);
			var que = new QuestionFacade().GetNumOfRandQuestionsFromThematicAreas(testTmp.NumOfQuestions, testTmp.ThematicAreas);

			var answs = que.SelectMany(s => s.Answers)
				.Select(tmp => new CheckboxModel {Id = tmp.Id, Selected = false})
				.ToArray();


			var model = new TestActiveModel
			{
				Id = testTmp.Id,
				Name = testTmp.Name,
				CompletionTime = testTmp.CompletionTime,
				Date = testTmp.Date,
				NumOfQuestions = testTmp.NumOfQuestions,
				Questions = que,
				Answers = answs
			};

				return View(model);
		}

		[HttpPost]
		public ActionResult TakeTest(int id ,TestActiveModel testActive)
		{
			
			var tmp = testActive.Answers
				.ToDictionary(checkboxModel => checkboxModel.Id, checkboxModel => checkboxModel.Selected);
			var str = new StringBuilder();
			
			double respts = 0;
			
			foreach (var questionDto in testActive.Questions)
			{
				var correctResult = 0;
				var correct = 0;
				var wrong = 0;
				var question = questionFacade.GetQuestionById(questionDto.Id);

				foreach (var answerDto in question.Answers)
				{
					
					if (answerDto.Correct && tmp.FirstOrDefault(s => s.Key == answerDto.Id ).Value == answerDto.Correct)
					{
						correctResult++;
					}
					else if(!answerDto.Correct && tmp.FirstOrDefault(s => s.Key == answerDto.Id).Value )
					{
						wrong++;
					}
					if (answerDto.Correct)
					{
						correct++;
					}
				}
				if (correct == 0)
				{
					continue;
				}
				var count =  (double)(correctResult - wrong) / correct ;
				if (count < 0) count = 0;
				count *= question.Points;


				respts += (Math.Round((double)(count * 4), MidpointRounding.ToEven) / 4);

			}
			
			foreach (var b in tmp)
			{
				
				str.Append(b.Key + "," + b.Value + ";");
			}

			resultFacade.CreateResult(new ResultDTO {
				TestTemplateId = id,
				UserId = Convert.ToInt32(User.Identity.GetUserId()),
				ResultString = str.ToString(),
				ResultPoints = respts,
				TestTemplateName = testActive.Name,
				Time = DateTime.Now


			});



			return RedirectToAction("Index");
		}


		public ActionResult CompletedTestsIndex()
		{


			if (User.IsInRole("admin"))
			{
				return View(resultFacade.GetAllResults());
			}
			if (User.IsInRole("student"))
			{
				return View(resultFacade.GetAllResultsFromUser(Convert.ToInt32(User.Identity.GetUserId())));
			}
			return RedirectToAction("Index", "Home");
		}

		public ActionResult OpenResult(int id)
		{

			var testResult = resultFacade.GetResultWithId(id);
			var resList = testResult.ResultString.Split(';');
			resList = resList.Take(resList.Length - 1).ToArray();
			var arrRes = resList.Select(s => s.Split(','))
				.Select(strings =>new CheckboxModel { Id = int.Parse(strings[0]), Selected = Convert.ToBoolean(strings[1]) })
				.ToArray();
			




			var testTmp = testFacade.GetTestTemplateById(testResult.TestTemplateId);
			var que = new QuestionFacade().GetListOfQuestionsByTheirAnswers(arrRes.Select(s => s.Id).ToList());
			var blres = new List<bool>();
			var answers = que.SelectMany(s => s.Answers).ToList();
			for (int i = 0; i < answers.Count(); i++)
			{
				blres.Add(answers[i].Correct == arrRes[i].Selected);
			}




			var model = new ResultModel
			{
				Id = testTmp.Id,
				Name = testTmp.Name,
				CompletionTime = testTmp.CompletionTime,
				Date = testTmp.Date,
				NumOfQuestions = testTmp.NumOfQuestions,
				Questions = que,
				Answers = arrRes,
				ResultBools = blres,
				Score = testResult.ResultPoints
				
			};

			return View(model);

		}


		public ActionResult DeleteResult(int id)
		{
			resultFacade.RemoveResult(resultFacade.GetResultWithId(id));
			return RedirectToAction("CompletedTestsIndex");
		}
	}
}
