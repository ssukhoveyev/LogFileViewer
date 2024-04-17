using LogFileViewer.Properties;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowConfig : Window
    {
        public WindowConfig()
        {
            InitializeComponent();

            switch (Settings.Default.Codepage)
            {
                case 1251: cbCodepage.SelectedIndex = 0; break;
                case 65001: cbCodepage.SelectedIndex = 1; break;
            }
        }

        private void cbCodepage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbCodepage.SelectedIndex)
            {
                case 0: Settings.Default.Codepage = 1251; break;
                case 1: Settings.Default.Codepage = 65001; break;
            }
            Settings.Default.Save();
        }
    }
}
