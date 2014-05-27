using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portal.Entities;

namespace Portal.Repositories
{
    public class BaseRepository
    {
        #region Constructor
        protected readonly PortalFreelancingEntities db;

        public BaseRepository(PortalFreelancingEntities db)
        {
            this.db = db;
        }
        #endregion
    }
}