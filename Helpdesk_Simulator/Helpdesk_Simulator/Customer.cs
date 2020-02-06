using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk_Simulator
{
    class Customer 
    {
        int serviceTime;

        public Customer()
        {
            this.serviceTime = (int)(-(HelpDesk.AVG_CUSTOMER_SERVICE_TIME) * Math.Log(1 - new Random().NextDouble()));
        }

        public int ServiceTime { get => serviceTime; set => serviceTime = value; }
    }
}
