using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDapper.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Family { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
