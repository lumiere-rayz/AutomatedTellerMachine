using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using Microsoft.AspNet.Identity;

namespace AutomatedTellerMachine.Models
{
    public class Transaction
    {
        
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        
        [Required]
        public int CheckingAccountId{ get; set; }
        public virtual CheckingAccount CheckingAccount { get; set; }

    }
}