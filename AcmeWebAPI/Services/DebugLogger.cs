using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace AcmeWebAPI.Services
{
    public class DebugLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Debug.WriteLine(message);
        }
    }
}