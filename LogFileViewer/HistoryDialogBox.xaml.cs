using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogFileViewer
{
    /// <summary>
    /// Interaction logic for HistoryDialogBox.xaml
    /// </summary>
    public partial class HistoryDialogBox : Window
    {
        public bool DialogResult;
        public string filePath;
        public HistoryDialogBox()
        {
            InitializeComponent();

            HistoryCls history = new HistoryCls();

            foreach (string filePath in history.GetFileList())
                listBox.Items.Add(filePath);
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.filePath = (string)listBox.SelectedValue;
            this.DialogResult = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.filePath = (string)listBox.SelectedValue;
            this.DialogResult = true;
            this.Close();
        }
    }
}
