using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace windows
{
    class Previewer
    {
        private int winsize = 500;
        private StreamReader s;
        //private string direct;
        private char[][] sequ;
        private char[][] qual;

        private string[][] sizes;
        private string[][] seqnames = new string[8192][];
        string temp;
        Filemanager f;
        public List<int> avgs = new List<int>();
        public ChartValues<ObservablePoint> ValA { get; set; }
        public ChartValues<ObservablePoint> ValB { get; set; }


        public Previewer(ChartValues<ObservablePoint> A, ChartValues<ObservablePoint> B)
        {
            ValA = A;
            ValB = B;
        }
        public void fileselector()
        {
            f = new Filemanager();
            s = f.fileselectordialg();

        }
        

        public void runRandomSampler()
        {
            fileselector();
            randomSampler(ValA, ValB);
        }

        public void randomSampler(ChartValues<ObservablePoint> A, ChartValues<ObservablePoint> B)
        {
            int y = 0;

            for(int x = 0; x < f.z.Count; x++)
            {
                while(y < f.z[x]-y)
                {
                    s.ReadLine();
                    s.ReadLine();
                    s.ReadLine();
                    s.ReadLine();
                    y++;
                }
                s.ReadLine();
                s.ReadLine();
                s.ReadLine();
                int z = 0;
                int c=s.Read();
                while (c != Convert.ToInt32('\n'))
                {

                    /*
                    James Logic For putting points in graph
                    */
                    if (avgs.Count<=z)
                    {
                        avgs.Add(Convert.ToInt32(c));
                        
                    }
                    else
                    {
                        avgs[z] = avgs[z] + Convert.ToInt32(c);
                    }

                    z++;
                    
                    c = s.Read();
                }
                y++;
            }
            for (int i = 0; i < avgs.Count; i++){
                avgs[i] = avgs[i] / f.z.Count;
                B.Add(new ObservablePoint(i+1, avgs[i]));
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Window1))
                    {
                        (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + avgs[i] + "\n";

                    }
                }

            }
        }

        /*
        public void randomSampler()
        {
            int x = 0;
            //s.BaseStream.Position= 


            sequ = new char[f.z.Count][];
            qual = new char[f.z.Count][];
            sizes = new string[f.z.Count][];
            /*
            while(x < (f.z.Count)-1 x<2000)
            {   
                
                for(int y = 0; y < f.z[x]; y++)
                {
                    s.ReadLine();
                    s.ReadLine();
                    s.ReadLine();
                    s.ReadLine();
                }
                int p = 0;
                sizes[x] = new string[512];
                temp = s.ReadLine();
                sizes[x] = temp.Split('=');

                s.ReadLine();
                s.ReadLine();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Window1))
                    {
                        (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + sizes[x][1] + "\n";

                    }
                }
                if (sizes[x][1] != null)
                {
                    p = Convert.ToInt32(sizes[x][1]);
                }
                
                qual[x] = new char[p];
                s.Read(qual[x], 0, p);
                
                string k = new string(qual[x]);
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Window1))
                    {
                        (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + k + "\n";

                    }
                }
                
                s.DiscardBufferedData();
                s.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                //s.BaseStream.Position = 0;
                x++;
                //f.z.RemoveAt(0);
            }
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + "Complete" + "\n";

                }
            }
            
            for (int y = 0; y < f.z[x]; y++)
            {
                s.ReadLine();
                s.ReadLine();
                s.ReadLine();
                s.ReadLine();
            }
            int p = 0;
            sizes[x] = new string[512];
            temp = s.ReadLine();
            sizes[x] = temp.Split('=');

            s.ReadLine();
            s.ReadLine();
            p = Convert.ToInt32(sizes[x][1]);
            qual[x] = new char[p];
            s.Read(qual[x], 0, p);
            string k = new string(qual[x]);
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + k + "\n";

                }
            }
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + x + "\n" + lnum[2] + "\n" + fnum[2] + "\n";

                }
            }
            
        }
        */




        public char[][] fileopener()
        {


            

            for (int i = 0; i < winsize; i++)
            {   // read and store k reads into arrays


                /*
                for(int j=0; j<512; j++)
                {   //initialize string arrays
                    sizes[j] = new string[512];
                    seqnames[j] = new string[512];
                }
                */
                sizes[i] = new string[512];
                seqnames[i] = new string[512];


                //read name line
                temp = s.ReadLine();
                 
                //split name line to find length of sequence
                sizes[i] = temp.Split('=');

                //split name line to find name of sequence
                seqnames[i] = temp.Split(' ');

                //make int x equal the length of sequence
                int x = Convert.ToInt32(sizes[i][1]);

                //initialize sequence
                sequ[i] = new char[x];

                //initualize quality
                qual[i] = new char[x];

                //read sequence
                //s.Read(sequ[i], 0, x);

                //read rest of line
                s.ReadLine();

                //read rest of line
                s.ReadLine();

                //read quality
                s.Read(qual[i], 0, x);

                //read rest of line
                s.ReadLine();


                /*
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Statusbox.Text = (window as MainWindow).Statusbox.Text + "\n" + seqnames[i][0] + " " + sizes[i][1] + "\n";

                    }
                }
                */

            }
            /*
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {

                    //(window as MainWindow).Statusbox.Text = (window as MainWindow).Statusbox.Text + "Reading Complete" + "\n";
                }
            }
            */

            return qual;

        }
    }
}
