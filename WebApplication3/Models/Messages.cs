using Chatter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public string Messages { get; set; }
        public ICollection<Message> Entry { get; set; }
        string Time = Timestamp(DateTime.Now);
        public static string Timestamp(DateTime value)
        {
            return value.ToString("YYMMDDHHmmss");
        }

        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public  virtual ApplicationUser IdentityUserLogin { get; set; }
        public  virtual ApplicationUser IdentityUserRole { get; set; }


    }
}