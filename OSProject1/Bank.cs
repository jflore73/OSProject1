using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject1
{
    internal class Bank
    {
        private int funds;
        public Bank()
        {
            this.funds = 100000;
        }
        public override string ToString()
        {
            return "Funds: " + this.funds;
        }
    }
}
