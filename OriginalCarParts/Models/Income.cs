using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace OriginalCarParts.Models
{
    public class Income
    {
        public int IncomeId { get; set; }

        // получение суммы прихода
        //[NotMapped]
        //public double Sum
        //{
        //    get
        //    {
        //        using (DatabaseContext db = new DatabaseContext())
        //        {
        //            double sum = db.IncomeDetails.Join(db.Price, i => i.PriceId, p => p.PriceId,
        //                (i, p) => new
        //                {
        //                    Id = i.IncomeId,
        //                    Price = p.PurchasePrice
        //                }).Where(p => p.Id == IncomeId).Sum(p => p.Price);
        //            return Math.Round(sum, 2);
        //        }
        //    }
        //}
        public DateTime Date { get; set; }
        //[NotMapped]
        //public int NumberOfLines
        //{
        //    get
        //    {
        //        return 1;
        //    }
        //}

        public int? VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public virtual ICollection<IncomeDetails> _IncomeDetails { get; set; }

        public Income()
        {
            _IncomeDetails = new List<IncomeDetails>();
        }
    }
}
