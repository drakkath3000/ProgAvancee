using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Helpdesk_Simulator
{
    class Tech
    {
        bool atWork = false;
        CustomerQueue customers;
        

        public bool AtWork { get => atWork; set => atWork = value; }
        public CustomerQueue Customers { get => customers; set => customers = value; }

        public void Work()
        {
            while (true)
            {
                if ((customers.GetCount() > 0) && (atWork == false))
                {
    
                        if (customers.GetFirst() != null)
                        {
                            Customer c = customers.GetFirst();
                            customers.RemoveCustomer();
                            atWork = true;
                            Thread.Sleep(c.ServiceTime);
                            atWork = false;
                        }
                    

                }
            }
        }

    }
}
