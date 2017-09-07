using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinMan.Data
{
    public class Target
    {
        public int TargetId { get; set; }
        public Category Category { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AllowedAmount { get; set; }
        public double SpentAmount { get; set; }
        public bool Failed { get { return AllowedAmount > SpentAmount; } }
    }
    public class TargetGenerator
    {
        public static Target GetCurrentTarget(Dictionary<Category, Double> transactions, Dictionary<Category, Double> currentTransactions, List<Target> prevThreeTargets = null)
        {
            var max = transactions.Values.Max();
            KeyValuePair<Category, Double> targetCategory = new KeyValuePair<Category, double>();
            if (prevThreeTargets == null)
            {
                targetCategory = transactions.FirstOrDefault(x => x.Value == transactions.Values.Max());
                if (targetCategory.Key == Category.Unknown)
                {
                    targetCategory = transactions.Where(t => t.Value < targetCategory.Value).OrderByDescending(x => x.Value).FirstOrDefault();
                }
            }

            DateTime now = DateTime.Now;
            return new Target
            {
                AllowedAmount = targetCategory.Value * 0.9 / 6.0,
                Category = targetCategory.Key,
                StartDate = DateTime.Now.AddDays(DateTime.Now.Day * -1),
                EndDate = new DateTime(now.Year, now.Month,
                                     DateTime.DaysInMonth(now.Year, now.Month)),
                SpentAmount = currentTransactions.ContainsKey(targetCategory.Key) ? -1 * currentTransactions[targetCategory.Key] : 0,

            };
        }

        public static List<Target> GetPreviousMockedTargets()
        {
            return new List<Target> { new Target{AllowedAmount = 2000,SpentAmount = 2500,Category=Category.Food,StartDate = new DateTime(2017,5,1),EndDate = new DateTime(2017,5,31)
                                },
            new Target{AllowedAmount = 1000,SpentAmount = 800,Category=Category.CommunicationEntertainment,StartDate = new DateTime(2017,6,1),EndDate = new DateTime(2017,6,30)
                                },
            new Target{AllowedAmount = 900,SpentAmount = 890,Category=Category.Transport,StartDate = new DateTime(2017,7,1),EndDate = new DateTime(2017,7,31)
                                },
            new Target{AllowedAmount = 10000,SpentAmount = 11000,Category=Category.Home ,StartDate = new DateTime(2017,8,1),EndDate = new DateTime(2017,8,31)
                                },
             new Target{AllowedAmount = 6000,SpentAmount = 5000,Category=Category.Shopping ,StartDate = new DateTime(2017,9,1),EndDate = new DateTime(2017,9,30)
                                },};
        }
    }


}
