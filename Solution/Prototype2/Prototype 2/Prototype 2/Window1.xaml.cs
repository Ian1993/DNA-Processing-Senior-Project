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


namespace windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
   
    public partial class Window1 : Window
    {
        private MainWindow mainWindow;
        // get; set required in both this cs file and in the graphhelper I think
        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public ChartValues<ObservablePoint> ValuesB { get; set; }

        // all valuesX will need to be initiated in the Window constructor
        public Window1(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            ValuesA = new ChartValues<ObservablePoint>();
            ValuesB = new ChartValues<ObservablePoint>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //requires constructor to add information, will need overloaded constructor to do more variables
            Graphhelper selection = new Graphhelper(ValuesA, ValuesB);
            selection.Single();

            // My test Class1
            // for now it loops and adds values to ValuesA and ValuesB
            //Class1 class1 = new Class1(ValuesA, ValuesB);
            //class1.Start();

            // DataContext is reqired here but not in the graphhelper
            DataContext = this;
        }
    }
}
