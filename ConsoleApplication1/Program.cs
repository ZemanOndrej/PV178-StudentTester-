using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.Facades;
using BL.Identity;
using DAL;
using DAL2try.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			var themAreaFac = new ThematicAreaFacade();
		
			var questionFac = new QuestionFacade();
			var studgrpFac = new StudentGroupFacade();
			var ansFac = new AnswerFacade();
			var userFac = new UserFacade();
			var testTempFac = new TestTemplateFacade();

			
			var que1 = new QuestionDTO()
			{
				Text = "EleGiggle"
				
			};
			var que2 = new QuestionDTO()
			{
				Text = "Keepo"
				
			};
			
			var pogChamp = new QuestionDTO()
			{
				Text = "Kappa"
				
			
				
			};
			var stud = new UserDTO
			{
			
				Email = "b@b.b",
				Name = "EleGiggle",
				Surname = "FrankerZ",
				Password = "b",
				Code = null,
				UserName = "SwiftRage",

			};
		
			

			var kappa = new StudentGroupDTO { Name = "StudGroup101" };
			kappa.Students.Add(stud);
			
			kappa.Tests.Add(new TestTemplateDTO {Name = "gachiGASM" , Date = DateTime.Now});
			var test = new TestTemplateDTO {Name = "dasdaaaaaaaaa",Date = DateTime.Now };

			

			var roleManager = new AppRoleManager(new AppRoleStore(new AppDbContext()));

			if (!roleManager.RoleExists("admin"))
			{
				roleManager.Create(new AppRole { Name = "admin" });
			}
			if (!roleManager.RoleExists("student"))
			{
				roleManager.Create(new AppRole { Name = "student" });
			}



			


			//			studgrpFac.CreateStudentGroup(kappa);
			//			questionFac.CreateManyQuestions(new List<QuestionDTO> { que1, que2, pogChamp }, "Twitch");


			//			userFac.Register(new UserDTO
			//						{
			//							Email = "aaaaa@aaa.aaa",
			//							Name = "Kapper",
			//							Surname = "PogChamp",
			//							Password = "aaaaaa",
			//							Code = "Kappa", 
			//							UserName = "KKona",
			//
			//						});
			//
			//
						var testTemp = new TestTemplateDTO
						{
							Name = "MyFirstRealTest",
							CompletionTime = new TimeSpan(1,1,1),
							Date = DateTime.Now,
							NumOfQuestions = 5,
							ThematicAreas = new List<ThematicAreaDTO>
							{
								new ThematicAreaDTO
								{
									Name = "Twitch"
								}
							}
			
						};






//			for (int i = 0; i < 5; i++)
//			{
//				var quest = new QuestionDTO
//				{
//					Answers = new List<AnswerDTO>
//				{
//					new AnswerDTO
//					{
//
//						Text = "asddasd "+i,
//						Correct = true
//					},
//					new AnswerDTO
//					{
//						Text = "fdaas da"+i,
//						Correct = false
//					},
//					new AnswerDTO
//					{
//						Text = "Tdasd "+i,
//						Correct = false
//					}
//					,
//					new AnswerDTO
//					{
//						Text = "Kasda "+i,
//						Correct = false
//					}
//				}
//				,
//					Text = "Qnum =>"+i,
//					OneAnswer = true,
//					Points = 420
//				};
//
//				questionFac.CreateQuestion(quest, "Muni");
//			}

			Console.WriteLine(testTempFac.CreateTestTemplate(new TestTemplateDTO
			{
				Name = "NewFag",NumOfQuestions = 5
			}));

			var quest=new QuestionDTO
			{
				Text = "AAAAAAAAAAAAAAA",
				
				Points = 2,OneAnswer = true
			};
			int id =questionFac.CreateQuestion(quest,"Twitch");
			var nuQ = questionFac.GetQuestionById(id);

			questionFac.AddAnswerToQuestion(nuQ,new AnswerDTO
			{
				Text = "aaaaaaaaaaaaaaaaaaaaaaaaa"
			});


			var nuQ2 = questionFac.GetQuestionById(id);
			Console.WriteLine("/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\");
			foreach (var answer in nuQ2.Answers)
			{
				Console.WriteLine("ans ->"+answer.Text);
			}



			//			var quest3 = questionFac.GetQuestionById(3);
			//
			//			 var ans = new AnswerDTO
			//			 {
			//				 Correct = true,
			//				 Description = " ayyy almao1",
			//				 Text = "TriHard",
			//				 Question = quest3
			//
			//			 };
			//			ansFac.CreateAnswer(ans);
			//
			//			ans = new AnswerDTO
			//			{
			//				Correct = true,
			//				Description = "Top KEK",
			//				Text = "4Head",
			//				Question = quest3
			//
			//			};
			//			ansFac.CreateAnswer(ans);
			//
			//
			//
			//
			//
			//			quest3 = questionFac.GetQuestionById(2);
			//
			//			ans = new AnswerDTO
			//			{
			//				Correct = true,
			//				Description = " ayyy almao2",
			//				Text = "haHAA",
			//				Question = quest3
			//
			//			};
			//			ansFac.CreateAnswer(ans);
			//
			//
			//
			//
			//			quest3 = questionFac.GetQuestionById(1);
			//
			//			ans = new AnswerDTO
			//			{
			//				Correct = true,
			//				Description = " ayyy almao3",
			//				Text = "KevinTurtle",
			//				Question = quest3
			//
			//			};
			//			ansFac.CreateAnswer(ans);
			//			testTempFac.CreateTestTemplate(testTemp);


			//testTempFac.UpdateTestTemplateTheme(testTemp,"LUL");

			//stud.StudentGroups.Add(kappa);
			//			studFac.CreateStudent(stud);
			//			teachFac.CreateTeacher(teacher);

			//			var test = kappa.Tests.First();
			//			test.Date=new DateTime(0);

			//			


			//teachFac.DeleteTeacher(teacher);



			//			questionFac.RemoveAllQuestions();
			//			themAreaFac.RemoveAllThematicAreas();
			//			testTempFac.RemoveAllTestTemplates();



			//						testTempFac.CreateTestTemplate(test);
			/*
						themAreaFac.CreateThematicArea("forsenW");
			
						themAreaFac.CreateThematicArea("forsenK");
						themAreaFac.CreateThematicArea("forsenC");
			
			
						questionFac.CreateQuestion(que1,"forsenW");
						questionFac.CreateQuestion(que2, "forsenW");
						questionFac.CreateQuestion(pogChamp, "ForsenC");
			*/
			//Console.WriteLine(questionFac.GetQuestionById(1).ThematicArea.Name+" kappa"+questionFac.GetQuestionById(1).Id);

			//studgrpFac.CreateStudentGroup(kappa);

			var tt=new TestTemplateDTO
			{
				Name = "asdf",
				NumOfQuestions = 5,
				ThematicAreas = new List<ThematicAreaDTO>
				{
					new ThematicAreaDTO
					{
						Name = "Twitch"
					},
					new ThematicAreaDTO
					{
						Name = "forsen"
					},
					new ThematicAreaDTO
					{
						Name="Billy"
					}
				}
			};
			
//			var tt2 = testTempFac.GetTemplateByName(tt.Name);
//
//			
//			Console.WriteLine("///////////");
//			tt2.ThematicAreas.Add(new ThematicAreaDTO
//			{
//				Name = "Billy"
//			});
//
//			testTempFac.UpdateTestTemplate(tt2);


			var themAreas = themAreaFac.GetAllThematicAreas();
			var questions = questionFac.GetAllQuestions();
			var studGrp = studgrpFac.GetAllStudentGroups();
			
			
			var allTests = testTempFac.GetAllTestTemplates();







			//			var qs = questionFac.GetNumOfRandQuestionsFromThematicAreas(5, testTemp.ThematicAreas);
			//			foreach (var q in qs)
			//			{
			//				Console.WriteLine(q.Text);
			//				
			//			}


//			foreach (var i in testTempFac.GetAllTestTemplates())
//			{
//				Console.WriteLine("testTemp name :" + i.Name);
//				foreach (var thematicAreaDto in i.ThematicAreas)
//				{
//					Console.WriteLine(thematicAreaDto.Name);
//				}
//			}

			Console.WriteLine("////////////////////////////");

//			Console.WriteLine("Stávající theme areas:");
//			foreach (var item in themAreas)
//			{
//				Console.WriteLine("   {0}  countQ:{1}", item.Name, item.Questions.Count);
//
//				foreach (var q in item.Questions)
//				{
//					Console.WriteLine("     "+q.Text);
//				}
//			}
//			Console.WriteLine("questions:");
//			foreach (var q in questions)
//			{
//				Console.WriteLine(@"   {0} area:{1}", q.Text, q.ThematicArea.Name);
//			}
//			Console.WriteLine("studgroups:");
//
//
//			foreach (var q in studGrp)
//			{
//				Console.WriteLine($"   studgrp name{ q.Name} ");
//
//				Console.WriteLine("      studs ");
//				foreach (var st in q.Students)
//				{
//					Console.WriteLine("        " + st.Name);
//				}
//				
//				Console.WriteLine("      tests ");
//				foreach (var st in q.Tests)
//				{
//					Console.WriteLine("        " + st.Name +"  DATE FAGGOTINA   "+ st.Date);
//					Console.WriteLine("kapaaaa");
//				}
//			}
//
//			
//
//
//			
//			Console.WriteLine(" studs :");
//			
//	
//
//			
//
//
//			Console.WriteLine(" testTemps :");
//			foreach (var q in allTests)
//			{
//				Console.WriteLine($"   testTemp name{ q.Name} " + "  DATE FAGGOTINA   " + q.Date);
//
//
//
//				Console.WriteLine("      themAreas ");
//				foreach (var st in q.ThematicAreas)
//				{
//					Console.WriteLine("        " + st.Name);
//				}
//			}
//
//
			Console.WriteLine("all done");
			Console.ReadKey();
		}
	}
}
