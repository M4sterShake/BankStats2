using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStats.Models
{
    public class SpendingByTypeModel
    {
        public string Type { get; set; }
        public decimal? PaidOut { get; set; }
    }
}