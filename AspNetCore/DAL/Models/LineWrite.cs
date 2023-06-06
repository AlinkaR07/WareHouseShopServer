using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace DAL.Models
{
    public class LineWrite
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Summa { get; set; }
        public int Count { get; set; }
        public int NumberActWrite_FK_ { get; set; }
        public int CodTovara_FK_ { get; set; }

        public virtual Write Write { get; set; }
        public virtual Tovar Tovar { get; set; }
    }
}
