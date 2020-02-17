using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOperation.lib
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public DateTime CreatedDate { get; set; }
        public int status_id { get; set; } // 1 - admin-teacher  2 - user-student
    }
}
