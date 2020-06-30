using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OriginalCarParts.Models
{
    public class IncomeDetails
    {
        public int IncomeDetailsId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]

        //[NotMapped]
        //public DateTime Data
        //{
        //    get
        //    {
        //        return DateTime.Now;
        //    }
        //}
        //[NotMapped]
        //public double Sum
        //{
        //    get
        //    {
        //        return 10;
        //    }
        //}
        public int? IncomeId { get; set; }
        public int? VendorId { get; set; }
        public int?  ItemId { get; set; }
        public int? PriceId { get; set; }

        public virtual Income Income { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Item Item { get; set; }
        public virtual Price Price { get; set; }
    }
}