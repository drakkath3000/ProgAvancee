using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Helpdesk_Simulator
{
    class Tech
    {
        bool atWork = false;
        List<Customer> customers = new List<Customer>();
        

        public bool AtWork { get => atWork; set => atWork = value; }
        public List<Customer> Customers { get => customers; set => customers = value; }

        public void Work()
        {
            if ((customers.Count > 0) && (atWork == false))
            {

            }
        }
    }
}
