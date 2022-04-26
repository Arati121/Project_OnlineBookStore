using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_OnlineBookStore.Models
{
    public class Orders
    {
        [Key]
        public int OId { get; set; }
        public int BId { get; set; }
        public int UId { get; set; }
    }
}
