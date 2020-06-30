using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OriginalCarParts.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Required, MinLength(2), MaxLength(25)]
        public string Name { get; set; }
        [Required]


        public virtual ICollection<Item> Items { get; set; }

        public Brand()
        {
            Items = new List<Item>();
        }
    }
}