using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsProject.Shared.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Photo { get; set; }
        
        public string Position { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Cabinet { get; set; }
        public DateTime? DateOfWork { get; set; }
        public bool InWork { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Person p2)
            {
                return Id == p2.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public List<NewsManagers> NewsManagers { get; set; } = new List<NewsManagers>();
    }
}
