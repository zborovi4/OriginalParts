using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OriginalCarParts.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required, MinLength(2), MaxLength(35)]
        public string ItemCode { get; set; }

        [Required, MinLength(2), MaxLength(120)]
        public string Description { get; set; }

        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<IncomeDetails> _IncomeDetails { get; set; }

        public Item()
        {
            Prices = new List<Price>();
            _IncomeDetails = new List<IncomeDetails>();
        }



    }
}