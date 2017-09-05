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
        public int CategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Amount { get; set; }
        public bool Complete { get; set; }


    }
}
