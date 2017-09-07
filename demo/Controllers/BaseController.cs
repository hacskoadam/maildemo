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
		protected int Leftmails;
		protected string LogedInUsername;

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);

			// Now you can access the HttpContext and User
			var userContext = new ApplicationDbContext();
			ViewData["Users"] = userContext.Users.Where(w => w.Hidden == false).ToList();
			ViewData["AllUsers"] = userContext.Users.ToList();

			if (User.Identity.IsAuthenticated)
			{
				
				LogedInUsername = User.Identity.GetUserName();
				var MaxMails = userContext.Users.Where(W => W.UserName == LogedInUsername).FirstOrDefault().DailyMailsMax;
				int usedToday = _db.Mails.Where(w => w.From == LogedInUsername).Where(w => w.SendDate.Day >= DateTime.Today.Day && w.SendDate.Month >= DateTime.Today.Month && w.SendDate.Year >= DateTime.Today.Year).Count();

				Leftmails = MaxMails - usedToday;
				ViewData["leftmailsCount"] = Leftmails;
			}
		}

		public BaseController()
		{

		}
	}
}