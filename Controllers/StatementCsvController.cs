using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FileHelpers;

namespace BankStats.Controllers
{
    public class StatementCsvController : Controller
    {
        //
        // GET: /StatementCsv/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase statementCsvFile)
        {
            if (statementCsvFile != null && statementCsvFile.ContentLength > 0)
            {
                string fileName = statementCsvFile.FileName;
                string csvServerPath = Path.Combine(Server.MapPath("../Content/Uploads/"), fileName);
                statementCsvFile.SaveAs(csvServerPath);
                this.parseCsvIntoDB(csvServerPath);
            }
            else
            {
                //say something went wrong.
                Logger.Log("Upload failed for some reason.");
            }
            return View("UploadStatementCsvResult");
        }

        private void parseCsvIntoDB(string csvFilePath)
        {
            using (BankStatsDBEntities2 db = new BankStatsDBEntities2())
            {
                FileHelperEngine csvReader = new FileHelperEngine(typeof(Models.BankTransactionParseStore));
                Models.BankTransactionParseStore[] bankTransactions = csvReader.ReadFile(csvFilePath) as Models.BankTransactionParseStore[];

                BankTransaction[] dbTransactions = new BankTransaction[bankTransactions.Length];

                string currentUserName = this.User.Identity.Name;
                Guid bankAccountID = new Guid();
                BankAccount defaultAccount = new BankAccount();
                defaultAccount.AccountName = "DefaultAccount";
                defaultAccount.AccountID = bankAccountID;
                defaultAccount.UserName = this.User.Identity.Name;
                db.BankAccounts.Add(defaultAccount);

                foreach(Models.BankTransactionParseStore parsedTransaction in bankTransactions)
                {
                    try
                    {
                        BankTransaction dbTransaction = new BankTransaction();
                        dbTransaction.Date = Convert.ToDateTime(parsedTransaction.Date);
                        dbTransaction.Type = parsedTransaction.Type;
                        dbTransaction.Description = parsedTransaction.Description;
                        dbTransaction.PaidIn = parsedTransaction.PaidIn == "-" ? 0 : Convert.ToDecimal(parsedTransaction.PaidIn);
                        dbTransaction.PaidOut = parsedTransaction.PaidOut == "-" ? 0 : Convert.ToDecimal(parsedTransaction.PaidOut);
                        dbTransaction.Ballance = Convert.ToDecimal(parsedTransaction.Ballance);
                        dbTransaction.TransactionID = Guid.NewGuid();
                        dbTransaction.BankAccountID = Guid.NewGuid();
                        dbTransaction.Target = getTransactionTarget(parsedTransaction.Description);
                        db.BankTransactions.Add(dbTransaction);
                    }
                    catch (Exception e)
                    {
                        Logger.Log("Could not parse one of the transactions: " + e.Message); 
                    }
                }
                
                try
                {
                    db.SaveChanges();
                }
                catch(InvalidOperationException e)
                {
                    Logger.Log("Unable to save the changes to the database: " + e.Message);
                }
            }
        }

        private string getTransactionTarget(string description)
        {
            //var datePattern = "\\d\\d";
            string transactionTarget = string.Empty;
            Match dateMatch = Regex.Match(description, @"\d{2}\w{3}\d{2}");
            if (dateMatch.Success)
            {
                transactionTarget = description.Replace(dateMatch.Groups[0].Value, "");
            }
            else 
            {
                transactionTarget = description;
            }
            return transactionTarget;
        }
    }
}
