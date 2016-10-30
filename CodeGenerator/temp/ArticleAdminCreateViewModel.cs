//Generate by CodeGenerator 2.0.0
using System;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace Panberes.ViewModels.Article
{
    public class ArticleAdminCreateViewModel
    {

        [Display(Name = @"Title")]
        [Required(ErrorMessage = @"require")]
        public String Title { get; set; }

        [Display(Name = @"Author")]
        [StringLength(40, ErrorMessage = @"stringle")]
        public String Author { get; set; }

        [Display(Name = @"IsApproved")]
        [Phone(ErrorMessage = @"phone")]
        public Boolean IsApproved { get; set; }

        [Display(Name = @"Summary")]
        [Range(0, 100, ErrorMessage = @"range")]
        public String Summary { get; set; }

        [Display(Name = @"Description")]
        [RegularExpression("patern", ErrorMessage = @"patern error")]
        public String Description { get; set; }

        [Display(Name = @"Source")]
        //[AllowHtml]
        [ScaffoldColumn(false)]
        public String Source { get; set; }

    }
}

