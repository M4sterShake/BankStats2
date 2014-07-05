using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStats.Models
{
    public class UserOverviewModel
    {
        public TransactionsByTypeModel TransactionsByType { get; set; }
        public BallanceByDateModel BallanceByDate { get; set; }
    }
}