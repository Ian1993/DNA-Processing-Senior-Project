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

            //create new chart value
            ValuesA = new ChartValues<ObservablePoint>();

            
           
            

            
        }
        
        public ChartValues<ObservablePoint> ValuesA { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //THIS BUTTON IS JUST FOR DEMONSTRATION PURPOSES
            //adds x and y to graph
            for (int i = 0; i < 60; i++) { 
            ValuesA.Add(new ObservablePoint(i, i*0.01));
        }

            //dont really know what this is but its the only way I could get it to work and it 
            //was in the livecharts example.
            DataContext = this;
        }
    }
}
