using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.Models
{
    public class NewsRating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public DateTime RatingDate { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public string UserId { get; set; }
    }
}
