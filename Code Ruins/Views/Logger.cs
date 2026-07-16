using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Code_Ruins.Views
{
    internal class Log(string saveRootPath,string saveFolderName)
    {
        string _saveRootPath = saveRootPath;
        string _saveFolderName = saveFolderName;
        

        void Write(string message,string type)
        {
            string savePath = Path.Combine(_saveRootPath, _saveFolderName);
            Directory.CreateDirectory(savePath);
            string timeNow = DateTime.Now.ToString("yyMMdd");
            string detailTimeNow = DateTime.Now.ToString("yyMMdd_HHmm");
            string fullMessage = $"[ {detailTimeNow} ] {type} : {message} + {Environment.NewLine}";
            File.AppendAllText(Path.Combine(savePath, (timeNow + ".log")), fullMessage);
        }
        public void Information(string message)
        {
            Write(message, "Info");
        }
        public void Warning(string message)
        {
            Write(message, "Warn");
        }
        public void Error(string message)
        {
            Write(message, "Error");
        }
    }
}
