//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portal.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Jobs = new HashSet<Job>();
            this.Jobs1 = new HashSet<Job>();
            this.JobsEmployes = new HashSet<JobsEmploye>();
        }
    
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public Nullable<long> Reputation { get; set; }
        public string Domains { get; set; }
        public Nullable<int> Experince { get; set; }
        public Nullable<long> Id_Project { get; set; }
        public Nullable<long> Id_Identity { get; set; }
        public Nullable<System.DateTime> LastAuth { get; set; }
    
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Job> Jobs1 { get; set; }
        public virtual ICollection<JobsEmploye> JobsEmployes { get; set; }
    }
}