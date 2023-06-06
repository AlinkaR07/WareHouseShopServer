using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public partial class Write
    {
        [Key]
        public int NumberAct { get; set; }
        public System.DateTime DataWrite { get; set; }
        public string FIOworker_FK_ { get; set; }
        public virtual ICollection<LineWrite>? LineWrites { get; set; }

        public Write()
        {
            LineWrites = new HashSet<LineWrite>();
        }
    }
}