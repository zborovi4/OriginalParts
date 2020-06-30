using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OriginalCarParts.Models
{
    public class OnePrice
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double  Price { get; set; }
        public int? VendorId { get; set; }
    }
}