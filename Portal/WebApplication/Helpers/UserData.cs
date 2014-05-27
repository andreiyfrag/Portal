using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Portal.Entities;

namespace Portal.Helpers
{
    [Serializable]
    public class UserData
    {
        public long Id { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime LastAuth { get; set; }

        public UserData(PortalFreelancingEntities db, string username)
        {
            LoadData(db, username);
        }

        public void LoadData(PortalFreelancingEntities db, string username)
        {
            var user = (from x in db.Users
                where x.Username == username
                select x).FirstOrDefault();
            user.LastAuth = DateTime.Now;
            db.SaveChanges();
            Id = user.Id;
            IsAdmin = user.Type == 1;
            Username = user.Username;
            Email = user.Email;
            Name = user.Lastname + " " + user.Firstname;
        }
    }
}