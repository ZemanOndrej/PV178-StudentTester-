using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Facades;
using Microsoft.AspNet.Identity;
using Web.Models;

namespace Web.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		UserFacade userFacade = new UserFacade();

		[Authorize(Roles = "admin")]
		public ActionResult Index()
		{
			return View(userFacade.GetAllUsers());
		}
		
		public ActionResult Create()
		{
			return RedirectToAction("Register","Account");
		}
	   

		
		public ActionResult Edit(int id)
		{
			if (User.IsInRole("admin") || id == int.Parse(User.Identity.GetUserId()))
			{
				var user = userFacade.GetUserById(id);
				return View(new EdirUserModel {User = user});
			}
			return RedirectToAction("Index", "Home");

		}

		
		[HttpPost]
		public ActionResult Edit(int id, EdirUserModel userModel, string save, string addGroup)
		{
			try
			{

				if (!string.IsNullOrEmpty(save))
				{


					var user = userFacade.GetUserById(id);

					user.Surname = userModel.User.Surname;
					user.Name = userModel.User.Name;
					user.UserName = userModel.User.UserName;
					

					userFacade.UpdateUser(user);

					return RedirectToAction("Index");

				}

				if (!string.IsNullOrEmpty(addGroup))
				{
					
					userFacade.AddStudentGroup(id, userModel.Group);
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
			userFacade.RemoveUser(id);
			return RedirectToAction("Index");
		}

		
		public ActionResult RemoveStudentGroup(int studgrpId, int userId)
		{
			userFacade.RemoveStudentGroup(studgrpId, userId);
			return RedirectToAction("Edit", new { id = userId });
		}
	}
}
