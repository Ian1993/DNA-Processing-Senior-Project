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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Forms;

namespace windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow mainWindow;

        public Window1(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;


            ChartValues<ObservablePoint> ValuesA = new ChartValues<ObservablePoint>();
            for(var i = 0; i < 8; i++)
            {
                ValuesA.Add(new ObservablePoint(i+1, i + 1));
            }
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


    }
}
