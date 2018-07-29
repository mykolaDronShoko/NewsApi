using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelsSPA.Models
{
    public class CommentsViewModel
    {
        public int Id_news { get; set; }
        public string Text { get; set; }
        public string User_name { get; set; }
        public string User_img { get; set; }
 
    }
}
