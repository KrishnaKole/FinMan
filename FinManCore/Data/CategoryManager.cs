using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMan.Data
{
    public class CategoryManager
    {
        private static Dictionary<Category, List<string>> Keywords =
            new Dictionary<Category, List<string>>() {
            { Category.Transport, new List<string>{"Ruter","NSB", "Taxi","Oil","Norwegian Air","Yellow" } },
            { Category.Food, new List<string>{"Mcdonald","Restaurant", "Restora", "Pizza","Coop", "Bunnpris","kebab","beer","Orange","Blue"} },
            { Category.Home, new List<string>{"Rent","Utleie", "Electricity","Hafslund","Strømberg Gruppen" } },
            { Category.Shopping, new List<string>{"Amazon","aliexpress", "ebay", "Storo Senter", "Steen &" } },
            { Category.Salary, new List<string>{"Salary", "lønn", "tata consultancy"} },
                {Category.CommunicationEntertainment,new List<string> { "Mycall","Skype","lyca","kino", "Netflix"} },
            };
        public static Dictionary<Category, Double> CategorizeTransactions(List<Transaction> transactions)
        {
            Dictionary<Category, Double> result = new Dictionary<Category, Double>();
            foreach (var transaction in transactions)
            {
                bool found = false;
                foreach (var key in Keywords)
                {
                    found = key.Value.Any(w => (!String.IsNullOrEmpty(transaction.messageKID) && transaction.messageKID.Contains(w)) || (!String.IsNullOrEmpty(transaction.transactionAccountName) && transaction.transactionAccountName.Contains(w)));
                    if (found)
                    {
                        if (result.ContainsKey(key.Key))
                        {
                            result[key.Key] += (transaction.amount * -1);

                        }
                        else
                        {
                            result[key.Key] = (transaction.amount * -1);
                        }
                        break;
                    }
                }
                if (!found)
                {
                    if (result.ContainsKey(Category.Overall))
                    {
                        result[Category.Overall] += (transaction.amount * -1);

                    }
                    else
                    {
                        result[Category.Overall] = (transaction.amount * -1);
                    }
                }
            }
            return result;
        }
    }

    public class TransactionCategory
    {
        public Category Category { get; set; }
        public Transaction Transaction { get; set; }
    }

    public class CategoryBalance
    {
        public Category Category { get; set; }
        public Double Balance { get; set; }
    }

    public enum Category
    {
        Transport,
        Food,
        Home,
        Shopping,
        CommunicationEntertainment,
        Salary,
        Overall
    }
}

