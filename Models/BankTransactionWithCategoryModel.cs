using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStats.Models
{
    public class BankTransactionWithCategoryModel
    {
        public BankTransactionWithCategoryModel()
        {

        }

        public string Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public string Category { get; set; }
        public string TransactionID { get; set; }
        public string PaidIn { get; set; }
        public string PaidOut { get; set; }
        public string Ballance { get; set; }
    }
}