using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.DataTransferObjects
{
    public class NewsUpdateDTO
    {
        public News News { get; set; }
        public List<Person> Managers { get; set; }
        public List<Image> Images { get; set; }
        public List<Category> SelectedCategories { get; set; }
        public List<Category> NotSelectedCategories { get; set; }
    }
}
