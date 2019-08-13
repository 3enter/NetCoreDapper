using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Class
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}
