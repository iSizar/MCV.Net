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
    
    public partial class CARTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARTE()
        {
            this.IMPRUMUT = new HashSet<IMPRUMUT>();
        }
    
        public int CarteId { get; set; }
        public Nullable<int> AutorId { get; set; }
        public string Titlu { get; set; }
        public Nullable<int> GenId { get; set; }
    
        public virtual AUTOR AUTOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMPRUMUT> IMPRUMUT { get; set; }
        public virtual GEN GEN { get; set; }
    }
}
