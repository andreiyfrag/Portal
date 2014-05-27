using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BForms.Models;
using BForms.Mvc;
using Portal.Resources;

namespace Portal.Areas.Authentification.Models
{
    public class AuthModels
    {
        public LoginModel Login { get; set; }

        public RegisterModel Register { get; set; }

        public bool IsWorking { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof (Resource))]
        [Display(Name = "UserName", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.TextBox)]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof (Resource))]
        [Display(Name = "Password", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.CheckBox)]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof (Resource))]
        [Display(Name = "Firstname", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Firstname { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof (Resource))]
        [Display(Name = "Lastname", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Lastname { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof (Resource))]
        [Display(Name = "Email", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.Email)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof (Resource))]
        [Display(Name = "UserName", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.TextBox)]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof (Resource))]
        [Display(Name = "Password", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof (Resource))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        

        [Display(Name = "RememberMe", ResourceType = typeof (Resource))]
        [BsControl(BsControlType.CheckBox)]
        public bool RememberMe { get; set; }
    }

}