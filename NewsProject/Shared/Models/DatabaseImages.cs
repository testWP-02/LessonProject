using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.Models
{
    public class DatabaseImages
    {
        public int ImageId { get; set; }
        public Image Image { get; set; }

        public int NewsId { get; set; }
        public News News { get; set; }

        public string Character { get; set; }
        public int Order { get; set; }
    }
}
