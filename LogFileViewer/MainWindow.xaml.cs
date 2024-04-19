using LogFileViewer.Properties;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace LogFileViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        LogFile logFile;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            switch (Settings.Default.Codepage)
            {
                case 65001: cbCodepage.SelectedIndex = 0; break;
                case 1251: cbCodepage.SelectedIndex = 1; break;
            }

            if (Settings.Default.PathFile != String.Empty && System.IO.File.Exists(Settings.Default.PathFile))
            {
                logFile = new LogFile(Settings.Default.PathFile);
                Update();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (logFile != null)
            {
                if (logFile.isModified())
                {
                    logFile.Update();
                    Update();
                }
            }
        }

        private void Update()
        {
            textBox.Text = logFile.logData;
            labelFileName.Content = logFile.filePath;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.MenuItem mi = (System.Windows.Controls.MenuItem)sender;
            if (mi.Header.ToString() == "Открыть")
                OpenFile_Click();
        }

        private void OpenFile_Click()
        {
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    logFile = new LogFile(openFileDialog.FileName);
                    Update();
                    Settings.Default.PathFile = openFileDialog.FileName;
                    Settings.Default.Save();
                }
            }
        }

        private void cbCodepage_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (cbCodepage.SelectedIndex)
            {
                case 0: Settings.Default.Codepage = 65001; break;
                case 1: Settings.Default.Codepage = 1251; break;
            }
            Settings.Default.Save();

            if (logFile != null)
            {
                logFile.Update();
                Update();
            }
        }
    }
}
