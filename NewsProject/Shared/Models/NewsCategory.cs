using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.Models
{
    public class NewsCategory
    {
        public int NewsId { get; set; }
        public News News { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
