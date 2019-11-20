namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Gallery")]
    public partial class Gallery
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string Header { get; set; }

        public string Description { get; set; }

        [StringLength(800)]
        public string Photo { get; set; }
    }
}
