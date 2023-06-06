using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.DAL.Models
{
    public partial class Tovar
    {
        [Key]
        public int CodTovara { get; set; }   // код товара
        public string Name { get; set; }  // название товара
        public DateTime? DateExpiration { get; set; } // срок годности
        public string Category { get; set; }  // категория
        public double Price { get; set; }  // цена
        public int Count { get; set; } // количество

        public Tovar()
        {

        }
    }
}
