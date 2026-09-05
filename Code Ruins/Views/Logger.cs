using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Specialized;

using AvaloniaEdit.Highlighting;

namespace Code_Ruins.Views
{
    public static class Log
    {
        private static string _saveRootPath = AppContext.BaseDirectory;



        private static string _saveFolderName = "log";
        public static void Init(string saveRootPath, string saveFolderName)
        {
            _saveRootPath = saveRootPath;
            _saveFolderName = saveFolderName;
        }
        private static void Write(string message, string type, Exception? ex = null)
        {
            string savePath = Path.Combine(_saveRootPath, _saveFolderName);
            Directory.CreateDirectory(savePath);
            string timeNow = DateTime.Now.ToString("yyMMdd");
            string detailTimeNow = DateTime.Now.ToString("yy-MM-dd HH:mm");
            string fullMessage = $"[ {detailTimeNow} ] {type} : {message} {Environment.NewLine}";
            if(ex is not null)
            {
                fullMessage += ex.StackTrace + ex.Message + Environment.NewLine;
            }
            /*if (OperatingSystem.IsWindows())
            {
                try
                {
                    File.AppendAllText(Path.Combine(savePath, timeNow + ".log"), fullMessage);
                }
                catch (UnauthorizedAccessException ex)
                {
                    string source = "CodeRuins";
                    string logName = "Application";
                    if (!EventLog.SourceExists(source))
                    {
                        EventLog.CreateEventSource(source, logName);
                    }
                    EventLog.WriteEntry(source, "日志写入权限错误", EventLogEntryType.Error);
                }

                catch (IOException ex)
                {
                    string source = "CodeRuins";
                    string logName = "Application";
                    if (!EventLog.SourceExists(source))
                    {
                        EventLog.CreateEventSource(source, logName);
                    }
                    EventLog.WriteEntry(source, "日志写入IO错误", EventLogEntryType.Error);
                }
                catch
                {
                    //没招了，滚去吧，不计也没事
                }
                return;
            }
            //我不知道别的操作系统，得了，就这样吧*/
            //上面的不管了
            File.AppendAllText(Path.Combine(savePath, timeNow + ".log"), fullMessage);


        }
        public static void Information(string message, Exception? ex = null)
        {
            Write(message, "Info", ex);
        }
        public static void Warning(string message, Exception? ex = null)
        {
            Write(message, "Warn", ex);
        }
        public static void Error(string message, Exception? ex = null)
        {
            Write(message, "Error", ex);
        }
    }
}