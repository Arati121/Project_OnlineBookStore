using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_OnlineBookStore.Models
{
    public class Users
    {
        [Required]
        [Key]
        public int UId { get; set; }
        public string UserName { get; set; }
        public string  EmailId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }



    }
}
