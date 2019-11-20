namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menyu")]
    public partial class Menyu
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Price { get; set; }

        [StringLength(200)]
        public string Ingridient { get; set; }

        [StringLength(800)]
        public string Photo { get; set; }

        public int? CategoryID { get; set; }

        public virtual MenuCategory MenuCategory { get; set; }
    }
}
