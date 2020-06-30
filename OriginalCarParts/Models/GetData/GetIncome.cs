using System;
using System.Linq;


namespace OriginalCarParts.Models.GetData
{
    public class GetIncome
    {

        public int IncomeId { get; set; }
        public DateTime Date { get;set; }
        public string  VendorId { get; set; }
        public string Vendor { get; set; }

        //количество записей в приходе
        public int NumberOfLine {
            get
            {
                using(DatabaseContext db = new DatabaseContext())
                {
                    int quantity = db.IncomeDetails.Count(i => i.IncomeId == IncomeId);
                    return quantity;
                }
                
            }
        }

        //сумма прихода
        public double SumIncome {
            get 
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    double sum = db.IncomeDetails.Join(db.Price, i => i.PriceId, p => p.PriceId,
                        (i, p) => new
                        {
                            Id = i.IncomeId,
                            Price = p.PurchasePrice
                        }).Where(p => p.Id == IncomeId).Sum(p => p.Price);
                    return Math.Round(sum, 2);
                }
            }

            set
            {
                SumIncome = value;
            }
        }
    }
}