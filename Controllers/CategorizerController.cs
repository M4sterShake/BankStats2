using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStats.Controllers
{
    public class CategorizerController : Controller
    {
        //
        // GET: /Categorizer/

        public ActionResult Index()
        {
            using (BankStatsDBEntities2 db = new BankStatsDBEntities2())
            {
                BankStats.Models.StatementModel statement = new BankStats.Models.StatementModel();
                List<BankTransaction> bankTransactionList = db.BankTransactions.ToList();
                List<BankStats.Models.BankTransactionWithCategoryModel> bankTransactionsWithCategories = new List<BankStats.Models.BankTransactionWithCategoryModel>();
                
                foreach(BankTransaction transaction in bankTransactionList)
                {
                    BankStats.Models.BankTransactionWithCategoryModel transactionWithCategory = new BankStats.Models.BankTransactionWithCategoryModel();
                    transactionWithCategory.Ballance = transaction.Ballance.ToString();
                    transactionWithCategory.Date = transaction.Date.ToString();
                    transactionWithCategory.Description = transaction.Description;
                    transactionWithCategory.PaidIn = transaction.PaidIn.ToString();
                    transactionWithCategory.PaidOut = transaction.PaidOut.ToString();
                    transactionWithCategory.Type = transaction.Type;
                    transactionWithCategory.TransactionID = transaction.TransactionID.ToString();
                    transactionWithCategory.Target = transaction.Target;

                    //Get category from TransactionCategory table.
                    TransactionCategory transactionCat = db.TransactionCategories.Find(transaction.Target);
                    transactionWithCategory.Category = transactionCat == null ? string.Empty :  transactionCat.TransactionCategory1;

                    bankTransactionsWithCategories.Add(transactionWithCategory);
                }

                statement.Transactions = bankTransactionsWithCategories;
                return View("Index", statement);
            }  
        }

        public ActionResult SetCategory(string category, string target)
        {
            using (BankStatsDBEntities2 db = new BankStatsDBEntities2())
            {
                TransactionCategory tg = new TransactionCategory();
                tg.TransactionCategory1 = category;
                tg.TransactionTarget = target;

                if (db.TransactionCategories.Find(target) != null)
                {
                    db.TransactionCategories.Find(target).TransactionCategory1 = category;
                }
                else
                {
                    db.TransactionCategories.Add(tg);
                }
                db.SaveChanges();
            }

            return Index();
        }
    }
}
