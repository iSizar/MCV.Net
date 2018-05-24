using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceBiblio
{
    [DataContract(IsReference =true)]
    public class AutorDTO
    {
        [DataMember]
        public int AutorId;

        [DataMember]
        public string Nume;

        [DataMember]
        public string Prenume;
    }
}
