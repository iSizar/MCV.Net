//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biblioteca_P1
{
    using System;
    using System.Collections.Generic;
    
    public partial class IMPRUMUT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IMPRUMUT()
        {
            this.REVIEW = new HashSet<REVIEW>();
        }
    
        public int ImprumutId { get; set; }
        public Nullable<int> CarteId { get; set; }
        public Nullable<int> CititorId { get; set; }
        public Nullable<System.DateTime> DataImprumut { get; set; }
        public Nullable<System.DateTime> DataScadenta { get; set; }
        public Nullable<System.DateTime> DataRestituire { get; set; }
    
        public virtual CARTE CARTE { get; set; }
        public virtual CITITOR CITITOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REVIEW> REVIEW { get; set; }
    }
}
