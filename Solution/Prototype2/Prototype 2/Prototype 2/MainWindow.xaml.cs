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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1(this);
            win1.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 win2 = new Window2(this);
            win2.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3(this);
            win3.Show();
            this.Hide();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult chose = MessageBox.Show("Close Data Processor?", "Data Processor exit conformation.", MessageBoxButton.OKCancel);
            switch (chose)
            {
                case MessageBoxResult.OK:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }



        }
    }
}
