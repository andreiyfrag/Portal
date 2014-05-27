using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Entities;
using Portal.Helpers;
using Portal.Repositories;
using RequireJS;

namespace Portal.Controllers
{
    public class BaseController : RequireJS.RequireJsController
    {
        #region Properties
        public PortalFreelancingEntities db = new PortalFreelancingEntities();
        public UserData GetUserData { get { return ((UserData)Session["UserData"]); } }
        public BaseRepository baseRepo { get; set; }
        #endregion

        protected BaseController()
        {
            baseRepo = new BaseRepository(db);
        }

        public override void RegisterGlobalOptions()
        {
            RequireJsOptions.Add(
                "homeUrl",
                Url.Action("Index", "Home", new { area = "" }),
                RequireJsOptionsScope.Global);
        }
    }
        
}