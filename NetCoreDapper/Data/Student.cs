using System;
using System.Collections.Generic;

namespace NetCoreDapper.Data
{
    public partial class Student
    {
        public Student()
        {
            StudentRoom = new HashSet<StudentRoom>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<StudentRoom> StudentRoom { get; set; }
    }
}
