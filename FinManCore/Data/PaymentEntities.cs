using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMan.Data
{
    public class InitiatePaymentRequest
    {
        public string debitAccountNumber { get; set; }
        public string creditAccountNumber { get; set; }
        public string message { get; set; }
        public string amount { get; set; }
        public string paymentDate { get; set; }
    }


    public class InitiatePaymentResponse
    {
        public string message { get; set; }
        public string paymentStatus { get; set; }
        public string paymentDate { get; set; }
        public string debitAccountNumber { get; set; }
        public string creditAccountNumber { get; set; }
        public int amount { get; set; }
        public long paymentIDNumber { get; set; }
    }

    public class InitiatePaymentWithKidRequest
    {
        public string debitAccountNumber { get; set; }
        public string creditAccountNumber { get; set; }
        public string kidNumber { get; set; }
        public string amount { get; set; }
        public string paymentDate { get; set; }
    }


    public class InitiatePaymentWithKidResponse
    {
        public string paymentStatus { get; set; }
        public string paymentDate { get; set; }
        public string debitAccountNumber { get; set; }
        public string creditAccountNumber { get; set; }
        public int amount { get; set; }
        public string kidNumber { get; set; }
        public long paymentIDNumber { get; set; }
    }


}
