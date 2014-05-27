using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BForms.Grid;
using BForms.Models;
using Portal.Areas.Admin.Models;
using Portal.Entities;
using Portal.Repositories;
using Portal.Helpers;

namespace Portal.Areas.Admin.Repositories
{
    public class UserSettings : BsGridRepositorySettings<UsersSearchModel>
    {
        public int OrganizationId { get; set; }
    }
    public class UsersRepository : BsBaseGridRepository<User, UsersGridRowViewModel>
    {

        #region Properties and Constructor
        private PortalFreelancingEntities db;

        public UserSettings Settings
        {
            get { return settings as UserSettings; }
        }

        public UsersRepository(PortalFreelancingEntities db)
        {
            this.db = db;
        }
        #endregion

        #region Mappers
        public Func<User, UsersGridRowViewModel> Map_User_UserGridRowViewModel()
        {
            return Reflection.ExpressionMapper<User, UsersGridRowViewModel>
            (
                x => new UsersGridRowViewModel()
                {
                    IsAdmin = x.Type == 1
                }
            ).Compile();
        }

        public Func<User, UsersDetailsModel> Map_Users_UsersDetailsModel()
        {
            return Reflection.ExpressionMapper<User, UsersDetailsModel>
            (
                x => new UsersDetailsModel()
                {
                   
                }
            ).Compile();
        }
        #endregion

        public override IQueryable<User> Query()
        {
            return Filter(Settings.Search, db.Users);
        }

        public IQueryable<User> Filter(UsersSearchModel search, IQueryable<User> query)
        {
           
            return query;
        }

        public override IOrderedQueryable<User> OrderQuery(IQueryable<User> query)
        {
            orderedQueryBuilder.OrderFor(row => row.Username, y => y.Username);
            orderedQueryBuilder.OrderFor(row => row.IsAdmin, y => y.Type);
            orderedQueryBuilder.OrderFor(row => row.Lastname, y => y.Lastname);
            orderedQueryBuilder.OrderFor(row => row.Firstname, y => y.Firstname);
            return orderedQueryBuilder.Order(query,users => users.OrderBy(x=>x.Firstname)); 
        }

        public override IEnumerable<UsersGridRowViewModel> MapQuery(IQueryable<User> query)
        {
            return query.Select(Map_User_UserGridRowViewModel());
        }

        public override void FillDetails(UsersGridRowViewModel row)
        {
            var result = db.Users.Where(x => x.Id == row.Id).Select(Map_Users_UsersDetailsModel()).FirstOrDefault();
            row.Details = result;
        }

        public List<UsersGridRowViewModel> ReadRows(List<long> ids)
        {
            return db.Users.Where(x => ids.Contains(x.Id)).Select(Map_User_UserGridRowViewModel()).ToList();
        }
    }
}