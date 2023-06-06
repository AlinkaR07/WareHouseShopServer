using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.DAL.Models
{
    public partial class Write
    {
        [Key]
        public int NumberAct { get; set; }   // номер акта списания
        public DateTime DataWrite { get; set; }  // дата списания
        public string FIOworker_FK_ { get; set; }  // ФИО работника склада
        public virtual ICollection<LineWrite>? LineWrites { get; set; }   // строки акта списания

        public Write()
        {
            LineWrites = new HashSet<LineWrite>();
        }
    }
}
