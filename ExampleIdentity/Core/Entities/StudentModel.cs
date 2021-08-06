using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleIdentity.Core.Entities
{
    public class StudentModel
    {
        [Key]
        public int? IdStudent { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Subjects { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Marks { get; set; }
        public DateTime Datecreated { get; set; }
    }
}
