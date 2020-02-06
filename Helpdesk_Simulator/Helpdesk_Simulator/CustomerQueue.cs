using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk_Simulator
{
    class CustomerQueue
    {
        private readonly object block = new object();
        List<Customer> customers = new List<Customer>();



        public void AddCustomer()
        {
            lock (block)
                customers.Add(new Customer());
        }
        public int GetCount()
        {
            lock (block)
                return customers.Count;
        }
        public void RemoveCustomer()
        {
            lock (block)
                customers.Remove(GetFirst());
        }
        public Customer GetFirst()
        {
            lock (block)
                if (customers.Count > 0)
                    return customers.First();
                else
                    return null;
        }


    }
}
