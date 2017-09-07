using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMan.Data
{

    public class Customer
    {
        public string personalNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
        public Address address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string idNumber { get; set; }
        public string idType { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }


    public class CustomerResponse
    {
        public string customerID { get; set; }
        public string personalNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string idNumber { get; set; }
        public string idType { get; set; }
        public Address address { get; set; }
    }


    public class UpdateCustomerRequestResponse
    {
        public string customerID { get; set; }
        public Address address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
    }   


}
