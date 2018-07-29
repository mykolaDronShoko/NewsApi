using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsSPA.Models
{
    public class Categories
    {
        [Key]
        public int Id_cat { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
