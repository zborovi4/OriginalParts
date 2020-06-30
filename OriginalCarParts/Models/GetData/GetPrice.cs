using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OriginalCarParts.Models.GetData
{
    // модель для хранение информации о позиции в прайсе
    public class GetPrice
    {
        public int PriceId { get; set; }
        public string ItemCode { get; set; }
        public string Brand { get; set; }
        public double PurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public string Description { get; set; }
        public int? VendorId { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
    }
}