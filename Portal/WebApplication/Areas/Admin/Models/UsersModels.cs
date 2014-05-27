using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BForms.Models;
using BForms.Mvc;
using Portal.Areas.Admin.Helpers;
using Portal.Resources;

namespace Portal.Areas.Admin.Models
{
    #region ViewModel
    public class UsersViewModel
    {
        [BsGrid(HasDetails = true)]
        [BsToolbar(Theme = BsTheme.Blue)]
        [Display(Name = "Users", ResourceType = typeof(Resource))]
        public BsGridModel<UsersGridRowViewModel> Grid { get; set; }

        [BsToolbar(Theme = BsTheme.Blue)]
        [Display(Name = "Users", ResourceType = typeof(Resource))]
        public BsToolbarModel<UsersSearchModel, UsersNewModel> Toolbar { get; set; }
    }
    #endregion

    #region Toolbar
    public class UsersSearchModel
    {
        [Display(Name = "Username")]
        [BsControl(BsControlType.TextBox)]
        public string Username { get; set; }

        [Display(Name = "Lastname")]
        [BsControl(BsControlType.TextBox)]
        public string Lastname { get; set; }

        [Display(Name = "Firstname")]
        [BsControl(BsControlType.TextBox)]
        public string Firstname { get; set; }

        [Display(Name = "Email")]
        [BsControl(BsControlType.TextBox)]
        public string Email { get; set; }
    }

    public class UsersNewModel
    {
        public UsersNewModel()
        {
            IsAdmin = new BsSelectList<YesNoValueTypes?>();
            IsAdmin.ItemsFromEnum(typeof(YesNoValueTypes), YesNoValueTypes.Both);
            IsAdmin.SelectedValues = YesNoValueTypes.Yes;
        }

        [Display(Name = "Username")]
        [BsControl(BsControlType.TextBox)]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [BsControl(BsControlType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lastname")]
        [BsControl(BsControlType.TextBox)]
        public string Lastname { get; set; }

        [Display(Name = "Firstname")]
        [BsControl(BsControlType.TextBox)]
        public string Firstname { get; set; }

        [Display(Name = "Email")]
        [BsControl(BsControlType.Email)]
        public string Email { get; set; }

        [BsControl(BsControlType.RadioButtonList)]
        [Display(Name = "Este Admin")]
        public BsSelectList<YesNoValueTypes?> IsAdmin { get; set; }

        public long Id { get; set; }
    }
    #endregion

    public class UsersGridRowViewModel : BsGridRowModel<UsersDetailsModel>
    {
        public long Id { get; set; }


        [BsGridColumn(Width = 3)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [BsGridColumn(Width = 3)]
        [Display(Name = "Este Admin")]
        public bool IsAdmin { get; set; }

        [BsGridColumn(Width = 3)]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [BsGridColumn(Width = 3)]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        public override object GetUniqueID()
        {
            return Id;
        }
    }

    public class UsersDetailsModel : UsersNewModel
    {
        public long Id { get; set; }
    }
}