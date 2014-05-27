using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal.Areas.Admin.Helpers
{
    public enum YesNoValueTypes
    {
        [Display(Name = "All")]
        Both = 1,
        [Display(Name = "Da")]
        Yes = 2,
        [Display(Name = "Nu")]
        No = 3
    }
}