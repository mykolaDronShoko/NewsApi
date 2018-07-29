using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelsSPA.Models
{
  //  [JsonObject(MemberSerialization.OptOut)]
    public class Images
    {
        [Key]
        public int Id_img { get; set; }
        public string Img_Url { get; set; }
        public DateTime DateAdeed { get; set; }
        public bool Active { get; set; }
        public int Id_news { get; set; }

        public virtual News IdNews { get; set; }
    }
}
