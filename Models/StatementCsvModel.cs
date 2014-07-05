using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace BankStats.Models
{
    public class StatementCsvModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime Date{ get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Transaction Type")]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Paid In")]
        public string PaidIn { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Paid Out")]
        public string PaidOut { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Ballance")]
        public string Ballance {get; set;}
    }
}