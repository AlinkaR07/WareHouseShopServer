using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public partial class Tovar
    {
        [Key]
        public int CodTovara { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateExpiration { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }

        public Tovar()
        {

        }
    }
}
