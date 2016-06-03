using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;

namespace Web.Controllers
{
	[Authorize]
	public class StudentGroupController : Controller
	{
		StudentGroupFacade studentGroupFacade = new StudentGroupFacade();
		public ActionResult Index()
		{
			return View(studentGroupFacade.GetAllStudentGroups());
		}

		[Authorize(Roles = "admin")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(StudentGroupDTO group)
		{
			try
			{
				studentGroupFacade.CreateStudentGroup(group);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		[Authorize(Roles = "admin")]
		public ActionResult Edit(int id)
		{
			return View();
		}

		
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

		[Authorize(Roles = "admin")]
		public ActionResult Delete(int id)
		{
			studentGroupFacade.DeleteStudentGroup(id);
			return RedirectToAction("Index");
		}

		
	}
}
