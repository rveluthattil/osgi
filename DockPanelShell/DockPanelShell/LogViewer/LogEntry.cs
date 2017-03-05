using System;
using log4net.Core;
using log4net.Util;

namespace LogViewer
{
    public class LogEntry
    {
        public LogEntry(LoggingEventData data)
        {
            Domain = data.Domain;
            ExceptionString = data.ExceptionString;
            Identity = data.Identity;
            Level = data.Level;
            LocationInfo = data.LocationInfo;
            LoggerName = data.LoggerName;
            Message = data.Message;
            Properties = data.Properties;
            ThreadName = data.ThreadName;
            TimeStamp = data.TimeStamp;
            UserName = data.UserName;
        }

        public string Domain { get; set; }
        public string ExceptionString { get; set; }
        public string Identity { get; set; }
        public Level Level { get; set; }
        public LocationInfo LocationInfo { get; set; }
        public string LoggerName { get; set; }
        public string Message { get; set; }
        public PropertiesDictionary Properties { get; set; }
        public string ThreadName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserName { get; set; }
    }
}