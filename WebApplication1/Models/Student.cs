using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Student
    {
        public int ID { get; set; }

        public int ClassID { get; set; }
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

    }
    public enum Gender
    {
        Male = 0,
        Female = 1,
    }
}
