using LogFileViewer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileViewer
{
    class HistoryCls
    {
        public List<string> GetFileList()
        {
            List<string> files = new List<string>();

            foreach(string fileName in Settings.Default.History.Split(';'))
                files.Add(fileName);

            return files;
        }

        public void AddFile(string filePath)
        {
            int maxHistoryLenght = 10;

            string s = Settings.Default.History;

            string[] arr = s.Split(';');

            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length-1] = filePath;

            while (arr.Length > maxHistoryLenght)
            {
                arr = arr.Skip(1).ToArray();
            };

            Settings.Default.History = string.Join(";", arr);
            Settings.Default.Save();
        }
    }
}
