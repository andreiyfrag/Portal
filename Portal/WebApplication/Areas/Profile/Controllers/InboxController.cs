using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Areas.Profile.Controllers
{
    public class InboxController : Controller
    {
        //
        // GET: /Profile/Inbox/
        public ActionResult Mail()
        {
            return View();
        }
	}
}