using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Areas.Profile.Controllers
{
    public class PaymentsController : Controller
    {
        //
        // GET: /Profile/Payments/
        public ActionResult MyPayments()
        {
            return View();
        }
	}
}