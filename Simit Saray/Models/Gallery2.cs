namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Gallery2
    {
        public int ID { get; set; }

        [StringLength(800)]
        public string Photo { get; set; }
    }
}
