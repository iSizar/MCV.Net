using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceBiblio
{
    [DataContract(IsReference = true)]
    public class CititorDTO
    {
        [DataMember]
        public int CititorId;

        [DataMember]
        public string Nume;

        [DataMember]
        public string Prenume;

        [DataMember]
        public string Adresa;

        [DataMember]
        public string Email;

        [DataMember]
        public byte[] Stare;
    }
}
