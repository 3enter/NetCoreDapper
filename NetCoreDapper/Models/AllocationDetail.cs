using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDapper.Models
{
    public class AllocationDetail
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string StudentFamily { get; set; }
    }
}
