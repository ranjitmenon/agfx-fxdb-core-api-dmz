﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service.Sms.SmsSender
{
    public class SmsModel
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
