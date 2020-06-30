using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OriginalCarParts.Models
{
    public class Vendor
    {
        public int VendorId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(10), MinLength(8)]
        public string Code { get; set; }
        [Required, MaxLength(30), MinLength(5)]
        public string PaymentAccount { get; set; }
        [Required]
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<IncomeDetails> _IncomeDetails { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public Vendor()
        {
            Prices = new List<Price>();
            _IncomeDetails = new List<IncomeDetails>();
            Incomes = new List<Income>();
        }
    }
}