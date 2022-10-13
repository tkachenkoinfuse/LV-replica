using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWithHangfire.DAL
{
    public class TelegramBotSettings
    {
        public string AccessToken { get; set; }
        public string ChatId { get; set; }
        public string LogLvl { get; set; }
        public string Source { get; set; }
    }
}
