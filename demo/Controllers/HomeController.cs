using demo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace demo.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		protected ApplicationDbContext userContext = new ApplicationDbContext();

		public ActionResult Index()
		{
			String username = User.Identity.GetUserName();
			var model = _db.Mails.Where(w=>w.To == username).ToList();
			return View(model);
		}

		public ActionResult SendMail(string id = "")
		{
			ViewData["MailTo"] = id;
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SendMail(Message Model)
		{
			
			
			var user = userContext.Users.Where(w => w.UserName == Model.To).Count();
			if(user == 1)
			{
				Model.From = User.Identity.GetUserName();
				Model.SendDate = DateTime.Now;
				_db.Mails.Add(Model);
				_db.SaveChanges();
				return RedirectToAction("index");
			}
			else
			{
				ViewData["Error"] = "Invalid username";
				return View(Model);
			}


			ViewData["MailTo"] = Model.To;
			return View(Model);
		}

		public ActionResult DisplayMailDetails(int id)
		{
			var model = _db.Mails.Find(id);
			model.IsReaded = true;
			_db.SaveChanges();

			return PartialView(model);
		}

		public JsonResult MarkMailToUnreaded(int id)
		{
			var user = User.Identity.GetUserName(); 

			var model = _db.Mails.Find(id);

			if (model.To == user)
			{
				if (model != null)
				{
					model.IsReaded = false;
					_db.SaveChanges();

					return new JsonResult()
					{
						Data = new { status = "success" },
						JsonRequestBehavior = JsonRequestBehavior.AllowGet
					};
				}
			}
			return new JsonResult()
			{
				Data = new { status = "error" },
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};
		}


		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}