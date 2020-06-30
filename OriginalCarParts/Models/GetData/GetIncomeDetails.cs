using System;


namespace OriginalCarParts.Models.GetData
{
    //модель для детального описания каждой строки в приходе
    public class GetIncomeDetails
    {
        private double price;
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price
        {
            get
            {
                return Math.Round(price, 2);
            }
            set
            {
                price = value;
            }
        }

    }

}