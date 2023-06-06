using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace AspNetCoreApp.DAL.Models.DTO
{
    public class LineWriteDTO
    {
        [Key]
        public int ID { get; set; }           // id строки акта списания
        public string Name { get; set; }      // название товара
        public double? Summa { get; set; }    //  сумма списания данного товара
        public int Count { get; set; }         //   количество списания
        public int NumberActWrite_FK_ { get; set; }        // номер акта списания
        public int CodTovara_FK_ { get; set; }    // код товара

    }
}
