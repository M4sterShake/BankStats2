using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStats.Models
{
    public class EarningByTypeModel
    {
        public string Type { get; set; }
        public decimal? PaidIn { get; set; }
    }
}