using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsProject.Shared.Models
{
    public class News
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Video { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        public string Poster { get; set; }
        public string NewsData { get; set; }
        public string TitleBrif
        {
            get
            {
                if (string.IsNullOrEmpty(Title))
                {
                    return null;
                }
                if(Title.Length > 60)
                {
                    return Title.Substring(0, 60) + "...";
                }
                else
                {
                    return Title;
                }
            }
        }
        public List<NewsCategory> NewsCategories { get; set; } = new List<NewsCategory>();
        public List<NewsManagers> NewsManagers { get; set; } = new List<NewsManagers>();
        public List<DatabaseImages> NewsImages { get; set; } = new List<DatabaseImages>();
    }
}
