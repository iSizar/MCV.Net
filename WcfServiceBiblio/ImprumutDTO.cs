using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceBiblio
{

    [DataContract(IsReference = true)]
    public class ImprumutDTO
    {
        [DataMember]
        public int ImprumutId;
        [DataMember]
        public Nullable<System.DateTime> DataImprumut;
        [DataMember]
        public Nullable<System.DateTime> DataScadenta;
        [DataMember]
        public Nullable<System.DateTime> DataRestituire;
        [DataMember]
        public CarteDTO carte;
        [DataMember]
        public CititorDTO cititor;

    }
}
