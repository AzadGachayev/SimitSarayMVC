namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text.RegularExpressions;

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
        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}", ID, Header);

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = Regex.Replace(str, @"\sə", "e");

            // cut and trim 
            str = str.Substring(0, str.Length <= 90 ? str.Length : 90).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }


        
        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
