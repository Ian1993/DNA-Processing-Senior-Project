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
        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public ChartValues<ObservablePoint> ValuesB { get; set; }
        public ChartValues<ObservablePoint> ValuesC { get; set; }
        public ChartValues<ObservablePoint> ValuesD { get; set; }
        public Previewer fileone;
        public Previewer filetwo;

        // I believe that we will need to create an overloaded constructor if they need to use four variables
        // for now test it out with just one
        public Graphhelper(ChartValues<ObservablePoint> A, ChartValues<ObservablePoint> B){
            ValuesA = A;
            ValuesB = B;
        }
    
        public void Single()
        {
            
            //ValuesA = new ChartValues<ObservablePoint>();
            //ValuesB = new ChartValues<ObservablePoint>();
            fileone = new Previewer();
            Driver(fileone,ValuesA,ValuesB);
        }
        // I think Single and Double will need to be turned into overloaded constructors.
        public void Double()
        {
            ValuesA = new ChartValues<ObservablePoint>();
            ValuesB = new ChartValues<ObservablePoint>();
            ValuesC = new ChartValues<ObservablePoint>();
            ValuesD = new ChartValues<ObservablePoint>();
            fileone = new Previewer();
            filetwo = new Previewer();

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
                B.Add(new ObservablePoint(y,avg));
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
                    C.Add(new ObservablePoint(y, Convert.ToInt16(qual1[x][y])));
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
