using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_OnlineBookStore.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int BId { get; set; }

        public string BName { get; set; }

        public string BInfo { get; set; }

        public int BPrice { get; set; }

        public string BAuthor { get; set; }

        public string image { get; set; }

       
        public int  CatId { get; set; }

        [NotMapped]
        public string ? CatName { get; set; }
    }
}
