using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OriginalCarParts.Models
{
    public class AllItems
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }

    }
}