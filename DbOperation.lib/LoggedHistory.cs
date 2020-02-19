using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOperation.lib
{
    public class LoggedHistory
    {

        public int LoggedUserId { get; set; }
        public DateTime LoggedTime { get; set; }

        public LoggedHistory(int loggedUserId)
        {
            this.LoggedUserId = loggedUserId;
            this.LoggedTime = DateTime.Now;
        }
    }
}
