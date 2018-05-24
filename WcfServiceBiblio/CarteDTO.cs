using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceBiblio
{
    [DataContract(IsReference = true)]
    public class CarteDTO
    {
        [DataMember]
        public int CarteId;

        [DataMember]
        public Nullable<int> AutorId;

        [DataMember]
        public string Titlu;

        [DataMember]
        public Nullable<int> GenId;

        [DataMember]
        public AutorDTO autor;

        [DataMember]
        public GenDTO gen;
    }
}
