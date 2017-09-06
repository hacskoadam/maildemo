using demo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Controllers
{
    public class BaseController : Controller
    {
		protected DemoDB _db = new DemoDB();

		public List<ApplicationUser> model;

		public BaseController()
		{
			var userContext = new ApplicationDbContext();
			ViewData["Users"] = userContext.Users.Where(w => w.Hidden == false).ToList();
			var username = User.Identity.GetUserName();

			var MaxMails = userContext.Users.Where(W => W.UserName == username).FirstOrDefault().DailyMailsMax;

			ViewData["leftmails"] = MaxMails - _db.Mails.Where(w => w.From == username && w.SendDate == DateTime.Today).Count();
		}
    }
}