using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceBiblio
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1 s = new Service1();
            List<ImprumutDTO> list = s.getAllImprumutsNotAlive();
            foreach (var imp in list)
            {
                Console.WriteLine("{0} {1} {2}", imp.ImprumutId, imp.cititor.Nume.Trim(), imp.carte.Titlu.Trim());
            }
        }
    }
}