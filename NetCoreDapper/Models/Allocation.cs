using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDapper.Models
{
    public class Allocation : Room
    {
        public AllocationDetail Det { get; }
        public List<AllocationDetail> Details { get; set; }
    }
}
