using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace windows
{
    class Graphhelper
    {
        public ChartValues<ObservablePoint> A { get; set; }
        public ChartValues<ObservablePoint> B { get; set; }
        public ChartValues<ObservablePoint> C { get; set; }
        public ChartValues<ObservablePoint> D { get; set; }
        public Previewer fileone;
        public Previewer filetwo;

        public ChartValues<ObservablePoint> returnA()
        {
            return A;
        }
        public ChartValues<ObservablePoint> returnB()
        {
            return B;
        }
        public Graphhelper(){
            
        }
    
        public void Single(/*ChartValues<ObservablePoint> B, ChartValues<ObservablePoint> C*/)
        {
            A = new ChartValues<ObservablePoint>();
            B = new ChartValues<ObservablePoint>();
            fileone = new Previewer();
            Driver(fileone,A,B);
        }
        public void Double()
        {
            //ValuesA = new ChartValues<ObservablePoint>();
            //ValuesB = new ChartValues<ObservablePoint>();
            //ValuesC = new ChartValues<ObservablePoint>();
            //ValuesD = new ChartValues<ObservablePoint>();
            //fileone = new Previewer();
            //filetwo = new Previewer();

        }
        public void Driver(Previewer A, ChartValues<ObservablePoint> B, ChartValues<ObservablePoint> C )
        {
            Char[][] qual1;
            A.fileselector();
            qual1 = A.fileopener();
            long avg = 0;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + "Loading Data" + "\n";

                }
            }
            for (int y = 0; y<100; y++)
            {
               for(int x = 0; x < 500; x++)
                {
                    avg = avg + Convert.ToInt64(qual1[x][y]);
                }
                avg = avg / 500;
                C.Add(new ObservablePoint(y,avg));
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Window1))
                    {
                        (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + "Average for nucleotide: " + y + " is "+ avg + "\n";

                    }
                }
            }
            String k = "";
            for (int x = 0; x < 500; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    B.Add(new ObservablePoint(y, Convert.ToInt16(qual1[x][y])));
                    k = k + qual1[x][y];
                    
                }
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Window1))
                    {
                        (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + "QualityString " + k + "\n";

                    }
                }
                k = "";
                
            }
            //DataContext = this;

        }
        public void increasesize(Previewer A)
        {
            A.increment();
        }

        public void sizereset(Previewer A)
        {
            A.reset();
        }
        public void usersize(Previewer A, int B)
        {
            A.userselected(B);
        }

    }
}
