using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BForms.Models;
using BForms.Mvc;
using Portal.Areas.Admin.Models;
using Portal.Areas.Admin.Repositories;
using Portal.Controllers;

namespace Portal.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        #region Constructor and Properties
        private readonly UsersRepository _usersRepository;

        public UsersController()
        {
            _usersRepository = new UsersRepository(db);
        }
        #endregion


        #region Pages
        public ActionResult Index()
        {
            var gridModel = _usersRepository.ToBsGridViewModel(new UserSettings()
            {
            });

            var viewModel = gridModel.Wrap<UsersViewModel>(x => x.Grid);

            viewModel.Toolbar = new BsToolbarModel<UsersSearchModel, UsersNewModel>()
            {
                Search = new UsersSearchModel(),
                New = new UsersNewModel()
            };

            #region PageOptions
            var options = new Dictionary<string, object>
            {
                {"pagerUrl", Url.Action("Pager")},
                {"getRowsUrl", Url.Action("GetRows")},
                {"updateUrl", Url.Action("Update")},
                //{"deleteUrl", Url.Action("Delete")},
            };

            RequireJsOptions.Add("index", options);
            #endregion

            return View(viewModel);
        }
        #endregion

        #region Ajax
        public BsJsonResult Pager(UserSettings settings, int? orgRouteId)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            var html = string.Empty;
            var count = 0;
            var viewModel = _usersRepository.ToBsGridViewModel(settings, out count).Wrap<UsersViewModel>(x => x.Grid);

            html = this.BsRenderPartialView("Grid/_Grid", viewModel);

            return new BsJsonResult(new
            {
                Count = count,
                Html = html
            }, status, msg);
        }

        public BsJsonResult GetRows(List<BsGridRowData<long>> items)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            var html = string.Empty;

            html = GetRowsHelper(items);

            return new BsJsonResult(new
            {
                RowsHtml = html
            }, status, msg);
        }
        #endregion

        #region Helpers
        [NonAction]
        public string GetRowsHelper(List<BsGridRowData<long>> items)
        {
            var ids = items.Select(x => x.Id).ToList();

            var rowsModel = _usersRepository.ReadRows(ids);

            var viewModel = _usersRepository.ToBsGridViewModel(rowsModel, row => row.Id, items)
                    .Wrap<UsersViewModel>(x => x.Grid);

            return this.BsRenderPartialView("Grid/_Grid", viewModel);
        }
        #endregion
    }
}