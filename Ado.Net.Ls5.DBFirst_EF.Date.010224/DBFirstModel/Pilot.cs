//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ado.Net.Ls5.DBFirst_EF.Date._010224.DBFirstModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pilot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pilot()
        {
            this.AirPlanes = new HashSet<AirPlane>();
        }
    
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AirPlane> AirPlanes { get; set; }
    }
}