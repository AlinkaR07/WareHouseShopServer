using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace AspNetCoreApp.DAL.Models.DTO
{
    public class LineOrderDTO
    {
        [Key]
        public int ID { get; set; }                   // id строки заказ
        public string Name { get; set; }             // название товара 
        public string PurchasePrice { get; set; }    // закупочная цена
        public int CountOrder { get; set; }           // количество заказа
        public int? CountShipment { get; set; }       // количество поставки
        public int? CodTovara_FK_ { get; set; }       // код товара
        public int NumberOrder_FK_ { get; set; }       // номер заказа
        public DateTime? DataManuf { get; set; }       // дата изготовления

    }
}
