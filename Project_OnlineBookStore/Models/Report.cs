using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_OnlineBookStore.Models
{
    [Table("Report")]
    public class Report
    {
        public int id { get; set; }
        public string Customername { get; set; }
        public int Total { get; set; }

    }
}
