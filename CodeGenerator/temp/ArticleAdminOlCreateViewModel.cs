//Generate by CodeGenerator 2.0.0
using System;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;
using System.Collections.Generic;

namespace Panberes.ViewModels.Article
{
    public class ArticleAdminOlCreateViewModel
    {

        public Int32 ArticleId { get; set; }

        [Display(Name = @"عنوان")]
        [Required(ErrorMessage = @"عنوانعنوان")]
        public String Title { get; set; }

        [Display(Name = @"نویسنده")]
        [StringLength(100, ErrorMessage = @"نویسندهنویسنده")]
        public String Author { get; set; }

        [Display(Name = @"خلاصه")]
        [Phone(ErrorMessage = @"خلاصهخلاصه")]
        public String Summary { get; set; }

        [Display(Name = @"توضیحات")]
        [ScaffoldColumn(false)]
        [Range(20, 30, ErrorMessage = @"توضیحاتتوضیحات")]
        public String Description { get; set; }

        [Display(Name = @"منبع")]
        //[AllowHtml]
        [RegularExpression("؟؟", ErrorMessage = @"منبعمنبع")]
        public String Source { get; set; }


        [Display(Name = @"زبان")]
        [Required(ErrorMessage = @"زبان را انتخاب نمایید")]
        public short Language { get; set; }

        //public List<OthereLanguage> CurrentLanguageList = new List<OthereLanguage>();

    }
}

