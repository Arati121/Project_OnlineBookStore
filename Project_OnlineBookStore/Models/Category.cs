using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_OnlineBookStore.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        public string CatName { get; set; }
    }
}
