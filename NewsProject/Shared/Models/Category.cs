using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsProject.Shared.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<NewsCategory> NewsCategories { get; set; } = new List<NewsCategory>();
    }
}
