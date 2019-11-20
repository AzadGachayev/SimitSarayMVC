namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeHeader")]
    public partial class HomeHeader
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string Head { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(800)]
        public string Logo { get; set; }

        [StringLength(800)]
        public string BackgroundImage { get; set; }
    }
}
