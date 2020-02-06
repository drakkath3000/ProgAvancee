using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk_Simulator
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            HelpDesk simulation = new HelpDesk();
            simulation.StartSimulation();
        }
    }
}
