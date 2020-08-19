using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.DataTransferObjects
{
    public class DetailsNewsDTO
    {
        public News News { get; set; }
        public List<Category> Categories { get; set; }
        public List<Person> Managers { get; set; }
        public List<Image> Images { get; set; }
        public double AverageVote { get; set; }
        public int UserVote { get; set; }
    }
}
