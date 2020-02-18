using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOperation.lib
{
    public class LoggedHistory
    {
        public string LoggedUser { get; set; }
        public DateTime LoggedTime { get; set; }

        public LoggedHistory(string loggedUser)
        {
            this.LoggedUser = loggedUser;
            this.LoggedTime = DateTime.Now;
        }
    }
}
