using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Code_Ruins.Views
{
    public static class Log
    {
        private static string? _saveRootPath = null;
        private static string? _saveFolderName = null;

        public static void Init(string saveRootPath, string saveFolderName)
        { 
            _saveRootPath = saveRootPath;
            _saveFolderName = saveFolderName;
        }
        private static void Write(string message,string type)
        {
            string savePath = Path.Combine(_saveRootPath, _saveFolderName);
            Directory.CreateDirectory(savePath);
            string timeNow = DateTime.Now.ToString("yyMMdd");
            string detailTimeNow = DateTime.Now.ToString("yy-MM-dd HH:mm");
            string fullMessage = $"[ {detailTimeNow} ] {type} : {message} {Environment.NewLine}";
            File.AppendAllText(Path.Combine(savePath, (timeNow + ".log")), fullMessage);
        }
        public static void Information(string message)
        {
            Write(message, "Info");
        }
        public static void Warning(string message)
        {
            Write(message, "Warn");
        }
        public static void Error(string message)
        {
            Write(message, "Error");
        }
    }
}
