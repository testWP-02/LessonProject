using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.Models
{
    public class NewsManagers
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int NewsId { get; set; }
        public News News { get; set; }

        public string Character { get; set; }
        public int Order { get; set; }
    }
}
