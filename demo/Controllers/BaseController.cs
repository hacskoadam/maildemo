using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Controllers
{
    public class BaseController : Controller
    {
		public List<ApplicationUser> model;
		public BaseController()
		{
			var userContext = new ApplicationDbContext();
			ViewData["Users"] = userContext.Users.Where(w => w.Hidden == false).ToList();
		}
    }
}