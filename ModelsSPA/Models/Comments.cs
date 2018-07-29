using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelsSPA.Models
{
   // [JsonObject(MemberSerialization.OptOut)]
    public class Comments
    {
        [Key]
        public int Id_com { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Active { get; set; }
        public int Id_news { get; set; }
        public string User_name { get; set; }
        public string User_Img { get; set; }
        public virtual News IdNews { get; set; }
      
    }
}
