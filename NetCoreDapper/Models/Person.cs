using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDapper.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PID { get; set; }
        public string Code { get; set; }
        public int? PersonCodeTypeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? MaritalStatusID { get; set; }
        public int? GenderID { get; set; }
        public int? PrefixID { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? EditDate { get; set; }
        public int? CUserID { get; set; }
        public int? EUserID { get; set; }
    }
}
