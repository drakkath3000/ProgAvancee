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
        public const int TURN_TIME = 1_500;
        public const int FACT_PROB = 10;
        CustomerQueue queue = new CustomerQueue();
        Tech[] technicians = new Tech[NB_TECHS];
        Thread[] threads = new Thread[NB_TECHS];
        int freeTech;
        int techAtWork;
        string responseFromServer = null;


        public HelpDesk()
        {
            for (int i = 0; i < NB_TECHS; i++)
            {
                technicians[i] = new Tech();
                threads[i] = new Thread(new ThreadStart(technicians[i].Work));
                technicians[i].Customers = queue;
                threads[i].Start();
            }
            
        }

        public void StartSimulation()
        {



            Stopwatch clock = new Stopwatch();
            bool factUpdate = true;
            int factProb;
            string customerWord;

            int customerArrival = new Random().Next(CUSTOMER_ARRIVAL_PROB);
            if (customerArrival == 1)
                queue.AddCustomer();

            
            while (clock.ElapsedMilliseconds < 160_000)//8h
            {
                if ((queue.GetCount() + techAtWork) == 1)
                    customerWord = "customer";
                else
                    customerWord = "customers";
                Thread.Sleep(TURN_TIME);
                customerArrival = new Random().Next(CUSTOMER_ARRIVAL_PROB);
                if (customerArrival == 1)
                    queue.AddCustomer(); 
                

               
                freeTech = 0;
                techAtWork = 0;
                foreach (Tech tech in technicians) 
                {
                    if (tech.AtWork == false)
                    {
                        freeTech++;
                    }
                }
                foreach (Tech tech in technicians)
                {
                    if (tech.AtWork == true)
                    {
                        techAtWork++;
                    }
                }              
                Console.Clear();
                Console.WriteLine("Queue is " + queue.GetCount());
                Console.WriteLine("");
                Console.WriteLine(freeTech + " technicians are free");
                Console.WriteLine(techAtWork + " technicians are working");
                Console.WriteLine("");
                Console.WriteLine("There is " + (queue.GetCount() + techAtWork) + " "+customerWord+" in the shop");
                Console.WriteLine("");
                Console.WriteLine("");

                if (factUpdate == true)
                {
                    factRequest();
                }
                Console.WriteLine("Interesting fact: " + responseFromServer);
                factProb = new Random().Next(FACT_PROB);
                if (factProb == 0)
                    factUpdate = true;
                else
                    factUpdate = false;
                EndTask();
            }
        }
        private async Task factRequest()
        {
            Task endTask = EndTask();
            await endTask;
            WebRequest request = WebRequest.Create("http://numbersapi.com/random");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            
            
        }
        private async Task EndTask()
        {

        }
    }
}
