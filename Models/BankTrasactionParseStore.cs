using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using FileHelpers;

namespace BankStats.Models
{
    [DelimitedRecord(",")]
    public class BankTransactionParseStore
    {

        public BankTransactionParseStore()
        {

        }

        public string Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string PaidIn { get; set; }
        public string PaidOut { get; set; }
        public string Ballance { get; set; }
    }
}