using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMan.Data
{
    public class CustomerAccount
    {
        public string customerID { get; set; }
        public Account[] accounts { get; set; }
    }

    public class Account
    {
        public long accountNumber { get; set; }
        public string accountType { get; set; }
        public int availableBalance { get; set; }
    }
}


