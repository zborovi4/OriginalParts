using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OriginalCarParts.Models
{
    public class Price
    {

        public int PriceId { get; set; }       

        //[NotMapped]
        //[MinLength(2), MaxLength(25)]
        //public string Brand
        //{
        //    get
        //    {
        //        return "Здесь будет бренд";
        //    }
        //}

        [Required]
        public double PurchasePrice { get; set; }
        [Required]
        public double SellingPrice { get; set; }
        //[NotMapped]
        //[MaxLength(120)]
        //public string Description
        //{
        //    get
        //    {
        //        return "Здесь будет описание запчасти";
        //    }
        //}

        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int? VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public virtual ICollection<IncomeDetails> _IncomeDetails { get; set; }

        public Price()
        {
            _IncomeDetails = new List<IncomeDetails>();
        }
    }
}