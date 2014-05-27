using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Areas.Profile.Controllers
{
    public class JobsController : Controller
    {
        //
        // GET: /Profile/Jobs/
        public ActionResult MyJobs()
        {
            return View();
        }
        public ActionResult FindJobs()
        {
            return View();
        }
        public ActionResult PotentialJobs()
        {
            return View();
        }
	}
}