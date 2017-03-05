using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UIShell.OSGi.Utility;
using log4net.Appender;
using log4net.Core;

namespace LogViewer
{
    public class LogRecorder : AppenderSkeleton
    {
        // Fields

        private string GetProperValue(string value1, string value2)
        {
            return string.IsNullOrEmpty(value1) ? value2 : value1;
        }

        private T GetProperValue<T>(T value1, T value2) where T : class
        {
            return value1 == default(T) ? value2 : value1;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            LoggingEventData logEventData;
            if (loggingEvent.MessageObject != null && loggingEvent.MessageObject.GetType() == typeof (LoggingEventData))
            {
                logEventData = (LoggingEventData) loggingEvent.MessageObject;

                logEventData.Domain = GetProperValue(logEventData.Domain, loggingEvent.Domain);
                logEventData.ExceptionString = GetProperValue(logEventData.ExceptionString,
                                                              loggingEvent.ExceptionObject == null
                                                                  ? string.Empty
                                                                  : loggingEvent.ExceptionObject.Message);
                logEventData.UserName = GetProperValue(logEventData.UserName, loggingEvent.UserName);
                logEventData.TimeStamp = loggingEvent.TimeStamp;
                logEventData.ThreadName = GetProperValue(logEventData.ThreadName, loggingEvent.ThreadName);
                logEventData.Properties = GetProperValue(logEventData.Properties, loggingEvent.Properties);
                logEventData.Message = GetProperValue(logEventData.Message,
                                                      loggingEvent.MessageObject == null
                                                          ? string.Empty
                                                          : loggingEvent.MessageObject.ToString());
                logEventData.LoggerName = GetProperValue(logEventData.LoggerName, loggingEvent.LoggerName);
                logEventData.LocationInfo = GetProperValue(logEventData.LocationInfo, loggingEvent.LocationInformation);
                logEventData.Level = GetProperValue(logEventData.Level, loggingEvent.Level);
                logEventData.Identity = GetProperValue(logEventData.Identity, loggingEvent.Identity);
            }
            else
            {
                logEventData = new LoggingEventData
                                   {
                                       Domain = loggingEvent.Domain,
                                       ExceptionString =
                                           loggingEvent.ExceptionObject == null
                                               ? string.Empty
                                               : loggingEvent.ExceptionObject.Message,
                                       UserName = loggingEvent.UserName,
                                       TimeStamp = loggingEvent.TimeStamp,
                                       ThreadName = loggingEvent.ThreadName,
                                       Properties = loggingEvent.Properties,
                                       Message =
                                           loggingEvent.MessageObject == null
                                               ? string.Empty
                                               : loggingEvent.MessageObject.ToString(),
                                       LoggerName = loggingEvent.LoggerName,
                                       LocationInfo = loggingEvent.LocationInformation,
                                       Level = loggingEvent.Level,
                                       Identity = loggingEvent.Identity
                                   };
            }
            LogRepository.Log(new LogEntry(logEventData));
        }
    }


    public static class LogRepository
    {
        private static readonly IList<LogEntry> logEntries;
        private static readonly ReadOnlyCollection<LogEntry> readOnlyLogEntries;

        static LogRepository()
        {
            logEntries = new List<LogEntry>();
            readOnlyLogEntries = new ReadOnlyCollection<LogEntry>(logEntries);
            Enabled = true;
        }

        public static ReadOnlyCollection<LogEntry> LogEntries
        {
            get { return readOnlyLogEntries; }
        }

        public static bool Enabled { get; set; }
        public static event EventHandler<EventArgs<LogEntry>> LogEntryAdded;
        public static event EventHandler LogCleared;

        public static void Log(LogEntry logEntry)
        {
            if (Enabled)
            {
                logEntries.Add(logEntry);
                OnLogEntryAdded(logEntry);
            }
        }

        public static void Log(object data)
        {
            if (Enabled)
            {
                var logEntry = new LogEntry(new LoggingEventData {Message = data.ToString()});

                logEntries.Add(logEntry);
                OnLogEntryAdded(logEntry);
            }
        }

        public static void Clear()
        {
            logEntries.Clear();
            OnLogCleared();
        }

        private static void OnLogEntryAdded(LogEntry logEntry)
        {
            if (LogEntryAdded != null)
            {
                LogEntryAdded(typeof (LogRecorder), new EventArgs<LogEntry>(logEntry));
            }
        }

        private static void OnLogCleared()
        {
            if (LogCleared != null)
            {
                LogCleared(typeof (LogRecorder), new EventArgs());
            }
        }
    }
}