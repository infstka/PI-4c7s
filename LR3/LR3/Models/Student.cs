using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LR3.Models
{
    // класс для связи с базой данных
    [Table("students")]
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
}