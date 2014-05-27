using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Controllers;

namespace Portal.Areas.Profile.Controllers
{
    public class DashController : BaseController
    {
        //
        // GET: /Profile/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}