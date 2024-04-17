﻿using LogFileViewer.Properties;
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
        /// <summary>
        /// Время последнего обновления файла
        /// </summary>
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
            int codepage;


            //codepage = 65001;//UTF-8
            //codepage = 1251;//Windows-1251

            codepage = Settings.Default.Codepage;

            logData = File.ReadAllText(this.filePath, Encoding.GetEncoding(codepage)); 
            LastWriteTime = File.GetLastWriteTime(this.filePath);
        }
    }
}
