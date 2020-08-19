using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsProject.Shared.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        [Required]
        public DateTime? DateOfUpload { get; set; }
        public List<DatabaseImages> DatabaseImages { get; set; } = new List<DatabaseImages>();

        [NotMapped]
        public string ImagesCharacter { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Image p2)
            {
                return Id == p2.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
