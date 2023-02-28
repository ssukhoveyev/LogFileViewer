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
        }

        private void LabelSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("111");
        }

        private void LabelOpen_MouseDown(object sender, MouseButtonEventArgs e)
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
    }
}
