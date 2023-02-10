using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

            logFile = new LogFile(@"test.txt");

            Update();
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
            MessageBox.Show("111");
        }

        private void LabelOpen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Открыть файл");
        }
    }
}
