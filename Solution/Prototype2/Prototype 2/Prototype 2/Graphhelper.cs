using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace windows
{
    class Graphhelper
    {
        public ChartValues<ObservablePoint> ValuesA { get; set;}
        public ChartValues<ObservablePoint> ValuesB { get; set; }
        public ChartValues<ObservablePoint> ValuesC { get; set; }
        public ChartValues<ObservablePoint> ValuesD { get; set; }
        public Previewer fileone;
        public Previewer filetwo;

        public Graphhelper(){
            
        }
    
        public void Single()
        {
            ValuesA = new ChartValues<ObservablePoint>();
            ValuesB = new ChartValues<ObservablePoint>();
            fileone = new Previewer();
            Driver(fileone,ValuesA,ValuesB);
        }
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

            for(int x = 0; x<100; x++)
            {
               for(int y = 0; y < 500; y++)
                {
                    avg = avg + Convert.ToInt64(qual1[x][y]);
                }
                avg = avg / 500;
                B.Add(new ObservablePoint(x,avg));
            }

            for (int x = 0; x < 500; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    C.Add(new ObservablePoint(y, Convert.ToInt16(qual1[x][y])));
                }
                
                
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
