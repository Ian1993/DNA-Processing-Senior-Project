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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Bio2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        
        private bool sing = false;
        private bool two = false;
        private bool twonpair = false;
        private FilemanagerFastq dataman1 = new FilemanagerFastq();
        private FilemanagerFastq dataman2 = new FilemanagerFastq();
        private int window;
        private int qualitymin;
        private int windowmin;
        private int failedno;
        public MainWindow()
        {
            InitializeComponent();
            //ValuesSource = new ChartValues<ObservablePoint>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(sing == true)
            {
                dataman1.fileselectordialg();
                dataman1.driver();
            }
            else if (two == true)
            {
                dataman1.fileselectordialg();
                dataman2.fileselectordialg();
                dataman1.driver();
                dataman2.driver();
                
            }
            else if (twonpair == true)
            {

            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Statusbox.Text = "Please select a setting" + "\n";

                    }
                }
            }

        }

        private void Single_Checked(object sender, RoutedEventArgs e)
        {
            sing = true;
            two = false;
            twonpair = false;
        }

        private void Two_Checked(object sender, RoutedEventArgs e)
        {
            sing = false;
            two = true;
            twonpair = false;

        }

        private void TwonPair_Checked(object sender, RoutedEventArgs e)
        {
            sing = false;
            two = false;
            twonpair = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
