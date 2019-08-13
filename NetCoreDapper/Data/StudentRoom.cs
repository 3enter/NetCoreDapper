using System;
using System.Collections.Generic;

namespace NetCoreDapper.Data
{
    public partial class StudentRoom
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int StudentId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Student Student { get; set; }
    }
}
