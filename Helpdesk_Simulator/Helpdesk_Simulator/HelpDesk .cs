using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Helpdesk_Simulator
{
    class HelpDesk
    {
        public const int NB_TECHS = 5;
        public const int AVG_CUSTOMER_SERVICE_TIME = 10_000;
        public const int CUSTOMER_ARRIVAL_PROB = 2;
        public const int TURN_TIME = 1000;
        public const int FACT_PROB = 10;
        CustomerQueue queue = new CustomerQueue();
        Tech[] technicians = new Tech[NB_TECHS];
        Thread[] threads = new Thread[NB_TECHS];
        int freeTech;
        int techAtWork;

        public HelpDesk()
        {
            for (int i = 0; i < NB_TECHS; i++)
            {
                technicians[i] = new Tech();
                threads[i] = new Thread(new ThreadStart(technicians[i].Work));
            }
        }

        public void StartSimulation()
        {



            Stopwatch clock = new Stopwatch();
            int customerArrival = new Random().Next(CUSTOMER_ARRIVAL_PROB);
            if (customerArrival == 1)
                queue.Count++;

            while (clock.ElapsedMilliseconds < 160_000)//8h
            {
                Thread.Sleep(TURN_TIME);
                customerArrival = new Random().Next(CUSTOMER_ARRIVAL_PROB);
                if (customerArrival == 1)
                    queue.Count++;
                freeTech = 0;
                techAtWork = 0;
                foreach (Tech tech in technicians) if (tech.AtWork == false)
                        freeTech++;
                foreach (Tech tech in technicians) if (tech.AtWork == true)
                        techAtWork++;
                Console.Clear();
                Console.WriteLine("Queue is " + queue.Count);
                Console.WriteLine(freeTech + " technicians are free");
                Console.WriteLine(techAtWork + " technicians are working");
                Console.WriteLine("OwO")
                /*WebRequest request = WebRequest.Create("https://numbersapi.com/random");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine(response.StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine("Interesting fact: "+responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();*/
                Thread customerManagement = new Thread(new ThreadStart(CustomerManagement));
                if (customerManagement.IsAlive == false)
                {
                    customerManagement.Start();
                }


            }
        }
        void CustomerManagement()
        {
            Customer[] customers = new Customer[queue.Count];
            for (int i = 0; i < queue.Count; i++)
            {
                customers[i] = new Customer();

            }
            foreach (Customer customer in customers)
            {
                int selectEmployee = new Random().Next(NB_TECHS);
                while (technicians[selectEmployee].AtWork == true)
                    selectEmployee = new Random().Next(NB_TECHS);
                if (technicians[selectEmployee].AtWork == false)
                {
                    Stopwatch serviceTime = new Stopwatch();
                    threads[selectEmployee].Start();
                    while (serviceTime.ElapsedMilliseconds < customer.ServiceTime)
                        technicians[selectEmployee].AtWork = true;
                    queue.Count--;
                    for (int i = 0; i < queue.Count; i++)
                    {
                        customers[i] = new Customer();

                    }                   
                    technicians[selectEmployee].AtWork = false;
                    threads[selectEmployee].Abort();
                }
            }
        }


    }
}
