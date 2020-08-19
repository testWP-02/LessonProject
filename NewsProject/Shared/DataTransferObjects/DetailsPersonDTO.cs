using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Shared.DataTransferObjects
{
    public class DetailsPersonDTO
    {
        public Person Person { get; set; }
        public List<News> News { get; set; }
    }
}
