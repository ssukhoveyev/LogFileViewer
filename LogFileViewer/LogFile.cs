using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileViewer
{
    public class LogFile
    {
        public string filePath { get; set; }
        public string logData { get; set; }
        private DateTime LastWriteTime;

        public LogFile (string filePath)
        {
            this.filePath = filePath;
            Update();
        }

        public bool isModified()
        {
            if (File.GetLastWriteTime(this.filePath) > LastWriteTime)
                return true;
            return false;
        }

        public void Update()
        {
            logData = File.ReadAllText(this.filePath);
            LastWriteTime = File.GetLastWriteTime(this.filePath);
        }
    }
}
