namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuCategory")]
    public partial class MenuCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuCategory()
        {
            Menyu = new HashSet<Menyu>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(600)]
        public string CategoryPhoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menyu> Menyu { get; set; }
    }
}
