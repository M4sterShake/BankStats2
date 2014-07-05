using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStats.Models
{
    public class StatementModel
    {
        public List<BankTransactionWithCategoryModel> Transactions { get; set; }
    }
}