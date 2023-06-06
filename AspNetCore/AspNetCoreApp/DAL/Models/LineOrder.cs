using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace AspNetCoreApp.DAL.Models
{
    public class LineOrder
    {
        [Key]
        public int ID { get; set; }         // id строки заказ
        public string Name { get; set; }     // название товара 
        public string PurchasePrice { get; set; }   // закупочная цена
        public int CountOrder { get; set; }   // количество заказа
        public int? CountShipment { get; set; }   // количество поставки
        public int? CodTovara_FK_ { get; set; }  // код товара

        [ForeignKey(nameof(ID))]
        public int NumberOrder_FK_ { get; set; }  // номер заказа
        public DateTime? DataManuf { get; set; }  // дата изготовления

        public virtual Order? Order { get; set; }
        public virtual Tovar? Tovar { get; set; }
    }
}
