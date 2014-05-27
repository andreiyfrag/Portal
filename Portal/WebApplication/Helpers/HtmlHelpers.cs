using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Controllers;

namespace Portal.Helpers
{
    public static class HtmlHelpers
    {
        #region GetUserData
        public static UserData GetUserData(this HtmlHelper helper)
        {
            return (helper.ViewContext.Controller as BaseController).GetUserData;
        }
        #endregion
    }
}