using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsSPA.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class News
    {
        public News()
        {
            Comments = new HashSet<Comments>();
            Images = new HashSet<Images>();
        }
        [Key]
        public int Id_news { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public string Rank { get; set; }
        public bool Active { get; set; }
        public int NumberViews { get; set; }
        public int Id_cat { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Images> Images { get; set; }
        public virtual Categories Categories { get; set; }

    }
}
