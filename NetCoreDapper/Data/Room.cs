using System;
using System.Collections.Generic;

namespace NetCoreDapper.Data
{
    public partial class Room
    {
        public Room()
        {
            StudentRoom = new HashSet<StudentRoom>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<StudentRoom> StudentRoom { get; set; }
    }
}
