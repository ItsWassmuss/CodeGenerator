//Generate by CodeGenerator 2.0.0
using System;
using System.ComponentModel.DataAnnotations;
using CodeGenerator.Model;

namespace Panberes.ViewModels.Article
{
    public class ArticleAdminOlIndexListViewModel : IAdminIndexListBaseViewModel
    {

        public override int BaseId
        {
            set { _baseId = value; }
            get { return Id; }
        }
        private int _baseId;

        public Int32 Id { get; set; }

        [Display(Name = @"Title")]
        public String Title
        {
            set { _title = value; }
            get
            {
                return _title != null ? _title.Length > 25 ? (_title.Substring(0, 25) + "...") : _title : _title;
            }
        }
        private String _title;

        [Display(Name = @"Author")]
        public String Author
        {
            set { _author = value; }
            get
            {
                var cleanText = "";// Html.HtmlDel(_author);
                return cleanText != null ? cleanText.Length > 52 ? (cleanText.Substring(0, 52) + "...") : cleanText : _author;
            }
        }
        private String _author;

        [Display(Name = @"Summary")]
        public string Summary
        {
            set { _summary = value; }
            get
            {
                if (_summary != null)
                    if (_summary.ToLower() == "true" || _summary.ToLower() == "false")
                        return Convert.ToBoolean(_summary) ? "ha" : "na";
                return _summary;
            }
        }
        private string _summary;


        [Display(Name = @"زبان")]
        public string Language
        {
            set { _language = value; }
            get
            {
                return _language != null ? _language.Length > 20 ? (_language.Substring(0, 20) + "...") : _language : _language;
            }
        }
        private string _language;

    }
}

