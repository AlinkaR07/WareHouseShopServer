using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.DAL.Models
{
    public partial class Order
    {
        [Key]
        public int Number { get; set; }    // номер заказа
        public DateTime? DataOrder { get; set; }   // дата заказа
        public DateTime? DataShipment { get; set; }  // дата поставки
        public string? StatusOrder { get; set; }   // статус заказа
        public string NameOrganizationPostavshik_FK_ { get; set; }   // название организации поставщика
        public string FIOworker_FK_ { get; set; }  // ФИО работника склада
        public virtual ICollection<LineOrder>? LineOrders { get; set; }   // строки заказа

        public Order()
        {
            LineOrders = new HashSet<LineOrder>();
        }
    }
}

