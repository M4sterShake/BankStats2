using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStats.Models
{
    public class TransactionsByTypeModel
    {
        public TransactionsByTypeModel()
        {
            this.spendingsByType = new List<SpendingByTypeModel>();
            this.earningsByType = new List<EarningByTypeModel>();
        }
        public List<SpendingByTypeModel> spendingsByType { get; set; }
        public List<EarningByTypeModel> earningsByType { get; set; }
    }
}