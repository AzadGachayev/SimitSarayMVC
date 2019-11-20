namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlogItem")]
    public partial class BlogItem
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string Header { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string AuthorName { get; set; }

        [StringLength(700)]
        public string Photo { get; set; }
    }
}
