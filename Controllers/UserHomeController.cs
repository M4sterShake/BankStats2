using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankStats.Models;

namespace BankStats.Controllers
{
    public class UserHomeController : Controller
    {
        //
        // GET: /UserHome/

        public ActionResult UserHome()
        {
            return View("UserHome");
        }

        public ActionResult Statistics()
        {
            using (BankStatsDBEntities2 db = new BankStatsDBEntities2())
            {
                TransactionsByTypeModel transactionsByType = GetTransactionsByType(db);
                BallanceByDateModel ballanceByDate = new BallanceByDateModel();
                ballanceByDate.BallanceByDate = db.BankTransactions.ToList().OrderBy(x => x.Date).ToList();
                UserOverviewModel userOverview = new UserOverviewModel();
                userOverview.TransactionsByType = transactionsByType;
                userOverview.BallanceByDate = ballanceByDate;
                //Dictionary<DateTime, decimal?> BallanceByDate = new Dictionary<DateTime,decimal?>();
                //Go through list of transactions putting the highest ballance from each date into the BallanceByDate dictionary.
                /*foreach (BankTransaction transaction in transactionList)
                {
                    decimal? previousBallance;
                    BallanceByDate.TryGetValue(transaction.Date, out previousBallance);
                    
                    //If there's already a ballance for this date.
                    if (previousBallance != null && previousBallance != new decimal())
                    {
                        //If this transactions ballance is higher then set the ballance for this date to the ballance of this transaction.
                        if (transaction.Ballance > previousBallance)
                        {
                            BallanceByDate[transaction.Date] = transaction.Ballance;
                        }
                    }
                    else
                    {
                        //If no ballance was found for this date in the dictionary then add this one.
                        BallanceByDate.Add(transaction.Date, transaction.Ballance);
                    }
                }*/


                return View("StatisticsOverview", userOverview);
            }
        }

        /// <summary>
        /// Retrieves lists of transactions grouped by transaction type.
        /// </summary>
        /// <returns>A TransactionsByTypeModel object containing the users transactions grouped by transaction type.</returns>
        private TransactionsByTypeModel GetTransactionsByType(BankStatsDBEntities2 db)
        {
            TransactionsByTypeModel transactionsByType = new TransactionsByTypeModel();
            List<BankTransaction> transactionList = db.BankTransactions.ToList();

            Dictionary<string, decimal?> spentTotals = new Dictionary<string, decimal?>();
            Dictionary<string, decimal?> earnedTotals = new Dictionary<string, decimal?>();

            foreach (BankTransaction t in transactionList)
            {
                if (t.PaidOut > 0)
                {
                    if (spentTotals.ContainsKey(t.Type))
                    {
                        spentTotals[t.Type] += t.PaidOut;
                    }
                    else
                    {
                        spentTotals.Add(t.Type, t.PaidOut);
                    }
                }
                else
                {
                    if (earnedTotals.ContainsKey(t.Type))
                    {
                        earnedTotals[t.Type] += t.PaidIn;
                    }
                    else
                    {
                        earnedTotals.Add(t.Type, t.PaidIn);
                    }
                }

            }

            foreach (string type in earnedTotals.Keys)
            {
                EarningByTypeModel earningByType = new EarningByTypeModel();
                earningByType.Type = type;
                earningByType.PaidIn = earnedTotals[type];
                transactionsByType.earningsByType.Add(earningByType);
            }

            foreach (string type in spentTotals.Keys)
            {
                SpendingByTypeModel spendingByType = new SpendingByTypeModel();
                spendingByType.Type = type;
                spendingByType.PaidOut = spentTotals[type];
                transactionsByType.spendingsByType.Add(spendingByType);
            }
            return transactionsByType;
        }
    }
}
