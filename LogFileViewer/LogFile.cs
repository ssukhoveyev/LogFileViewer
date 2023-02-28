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
            logData = File.ReadAllText(this.filePath);

            //Encoding cp1251 = Encoding.GetEncoding(1251);
            //Encoding unicode = Encoding.Unicode;

            //byte[] cp1251Bytes = cp1251.GetBytes(logData);
            //byte[] unicodeBytes = Encoding.Convert(cp1251, unicode, cp1251Bytes);

            //char[] asciiChars = new char[unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
            //unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, asciiChars, 0);
            //string asciiString = new string(asciiChars);

            //char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            //ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            //string asciiString = new string(asciiChars);


            //logData = asciiString;

            logData = Win1251ToUTF8(logData);

            LastWriteTime = File.GetLastWriteTime(this.filePath);
        }

        static private string Win1251ToUTF8(string source)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding utf8 = Encoding.UTF8;
            Encoding win1251 = CodePagesEncodingProvider.Instance.GetEncoding(1251);
            byte[] utf8Bytes = win1251.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);
            source = win1251.GetString(win1251Bytes);
            return source;
        }
    }
}
