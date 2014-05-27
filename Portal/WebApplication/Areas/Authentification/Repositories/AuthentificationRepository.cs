using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portal.Areas.Authentification.Models;
using Portal.Entities;
using Portal.Repositories;

namespace Portal.Areas.Authentification.Repositories
{
    public class AuthentificationRepository: BaseRepository
    {
        public AuthentificationRepository(PortalFreelancingEntities db) : base(db)
        {
            
        }

        public bool Login(AuthModels model)
        {
            return
                db.Users.Any(
                    x =>
                        x.Username.ToLower().Contains(model.Login.UserName.ToLower()) &&
                        x.Password.ToLower().Contains(model.Login.Password.ToLower()));
        }

        public int? Register(AuthModels model)
        {
            return null;
        }
    }
}