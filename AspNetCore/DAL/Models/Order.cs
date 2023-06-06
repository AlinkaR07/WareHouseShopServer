using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;




namespace DAL.Models
{
    public partial class Order
    {
        [Key]
        public int Number { get; set; }
        public DateTime? DataOrder { get; set; }
        public DateTime? DataShipment { get; set; }
        public string? StatusOrder { get; set; }
        public string NameOrganizationPostavshik_FK_ { get; set; }
        public string FIOworker_FK_ { get; set; }
        public virtual ICollection<LineOrder>? LineOrders { get; set; }

        public Order()
        {
            LineOrders = new HashSet<LineOrder>();
        }
    }
}