using System;
//Generate by CodeGenerator 2.0.0
using System.ComponentModel.DataAnnotations;

namespace Panberes.ViewModels.Article
{
    public class ArticleAdminDetailsViewModel
    {

        Int32 Id { get; set; }

        [Display(Name = @"UserId")]
        String UserId { get; set; }

        [Display(Name = @"Title")]
        String Title { get; set; }

        [Display(Name = @"Author")]
        String Author { get; set; }

        [Display(Name = @"IsApproved")]
        Boolean IsApproved { get; set; }

        [Display(Name = @"Summary")]
        String Summary { get; set; }

        [Display(Name = @"Description")]
        String Description { get; set; }

        [Display(Name = @"Source")]
        String Source { get; set; }

        [Display(Name = @"ViewCount")]
        Int32 ViewCount { get; set; }

        [Display(Name = @"CreatedOn")]
        DateTime CreatedOn { get; set; }

        [Display(Name = @"CreatedOnPersian")]
        String CreatedOnPersian { get; set; }

        [Display(Name = @"CreatedBy")]
        String CreatedBy { get; set; }


        //public System.Collections.Generic.List<ArticleAdminOlDetailsViewModel> OlDetails;
    }
}

