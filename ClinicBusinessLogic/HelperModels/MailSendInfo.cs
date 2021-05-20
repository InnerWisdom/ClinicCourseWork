using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicBusinessLogic.HelperModels
{
    public class MailSendInfo
    {
        public string MailAddress { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public string File { get; set; }
    }
}
