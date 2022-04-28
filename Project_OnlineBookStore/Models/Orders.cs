using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_OnlineBookStore.Models
{

    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int OId { get; set; }
        public int UId { get; set; }
        public int BId { get; set; }
        public string BName { get; set; }
        public int BPrice { get; set; }
        public int Ouantity { get; set; }
        public int TotalBill { get; set; }
        public DateTime Orderdate { get; set; }
       

    }
}
