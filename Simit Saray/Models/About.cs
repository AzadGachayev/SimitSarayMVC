namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("About")]
    public partial class About
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Header { get; set; }

        [StringLength(800)]
        public string Photo { get; set; }

        [StringLength(800)]
        public string BackgroundImage { get; set; }

        public string Description { get; set; }

        [StringLength(150)]
        public string WorkTime { get; set; }
    }
}
