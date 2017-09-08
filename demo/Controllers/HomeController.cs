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
			var model = _db.Mails.Where(w => w.To == username).OrderByDescending(o => o.SendDate).ToList();
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
			string to = Model.To;
			int firstBrace = Model.To.IndexOf("(");
			int lastBrace = Model.To.IndexOf(")");
			if (firstBrace != -1 && lastBrace != -1)
			{
				to = Model.To.Substring(firstBrace + 1, lastBrace - firstBrace - 1);
			}

			var user = userContext.Users.Where(w => w.UserName == to).Count();//is valid user to send message
			if (Leftmails > 0)
			{
				if (user == 1)
				{
					string userFrom = User.Identity.GetUserName();
					if (_db.Blocks.Where(w => w.Who == to && w.Whom == userFrom).Count() == 0)
					{
						Model.From = userFrom;
						Model.SendDate = DateTime.Now;
						Model.To = to;
						_db.Mails.Add(Model);
						_db.SaveChanges();
						return RedirectToAction("index");
					}
					else
					{
						ViewData["Error"] = "This user is blocked you";
						return View(Model);
					}
				}
				else
				{
					ViewData["Error"] = "Invalid username";
					return View(Model);
				}
			}
			else
			{
				ViewData["Error"] = "You reached the daily mail limit";
				return View(Model);
			}
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

		public ActionResult SentMails()
		{
			string username = User.Identity.GetUserName();
			var model = _db.Mails.Where(w => w.From == username).OrderByDescending(o => o.SendDate).ToList();
			return View(model);
		}

		public ActionResult DisplayOwnMailDetails(int id)
		{
			var model = _db.Mails.Find(id);
			_db.SaveChanges();

			return PartialView(model);
		}

		[HttpPost]
		public JsonResult ToggleHideUser()
		{
			var userContext = new ApplicationDbContext();
			var AppUser = userContext.Users.Where(w => w.UserName == LogedInUsername).FirstOrDefault();
			if (AppUser != null)
			{
				AppUser.Hidden = !AppUser.Hidden;
				userContext.SaveChanges();

				return new JsonResult()
				{
					Data = new { status = "success" }
				};
			}
			return new JsonResult()
			{
				Data = new { status = "error" }
			};
		}

		public ActionResult InboxListPartial()
		{
			String username = User.Identity.GetUserName();
			var model = _db.Mails.Where(w => w.To == username).OrderByDescending(o => o.SendDate).ToList();
			return PartialView(model);
		}

		public JsonResult BlockUserByName(string name)
		{
			string actualuser = User.Identity.GetUserName();
			string userToBlock = "";
			int firstBrace = name.IndexOf("(");
			int lastBrace = name.IndexOf(")");

			if (firstBrace != -1 && lastBrace != -1)
			{
				userToBlock = name.Substring(firstBrace + 1, lastBrace - firstBrace - 1);
				if (_db.Blocks.Where(w => w.Who == actualuser && w.Whom == userToBlock).Count() == 0)
				{
					var newBlock = new Block()
					{
						Who = actualuser,
						Whom = userToBlock
					};
					_db.Blocks.Add(newBlock);
					_db.SaveChanges();

				}
				else
				{
					return new JsonResult()
					{
						Data = new { status = "allreadyBlocked" }
					};
				}
				return new JsonResult()
				{
					Data = new { status = "success" }
				};
			}
			return new JsonResult()
			{
				Data = new { status = "error" }
			};
		}

		public JsonResult UnBlockUser(string name)
		{
			string actualuser = User.Identity.GetUserName();
			string userToUnBlock = "";
			int firstBrace = name.IndexOf("(");
			int lastBrace = name.IndexOf(")");

			if (firstBrace != -1 && lastBrace != -1)
			{
				userToUnBlock = name.Substring(firstBrace + 1, lastBrace - firstBrace - 1);
				var items = _db.Blocks.Where(w => w.Who == actualuser && w.Whom == userToUnBlock);
				_db.Blocks.RemoveRange(items);
				_db.SaveChanges();
				return new JsonResult()
				{
					Data = new { status = "success" }
				};
			}
			return new JsonResult()
			{
				Data = new { status = "error" }
			};
		}
	}
}