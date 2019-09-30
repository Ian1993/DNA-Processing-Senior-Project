using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

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
        private string[][] seqnames = new string[max][];
        string temp;
        Filemanager f;
        private static int max = 8192;
        

        public void increment()
        {
            winsize = winsize + 500;
        }
        public void reset()
        {
            winsize = 500;
        }
        public void userselected(int use){
            winsize = use;
        } 

        public void fileselector()
        {
            f = new Filemanager();
            s = f.fileselectordialg();
            //s.Close();

        }
        /*
        public void highestReadfinder()
        {
            s.Seek();
        }
        */
        public void randomSampler()
        {
            int x = 0;
            //s.BaseStream.Position=  
            sizes = new string[f.z.Count][];
            qual = new char[f.z.Count][];
            while(x < f.z.Count-1)
            {
                //s = new StreamReader(f.direct);

                for (int y = 0; y < f.z[x]; y++)
                {
                    s.ReadLine();
                    s.ReadLine();
                    s.ReadLine();
                    s.ReadLine();
                }
                
                /*
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Window1))
                    {
                        (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + k + "\n";

                    }
                }
                */
                //s.Close();
                if (s.ReadLine() == null)
                {
                    s.DiscardBufferedData();
                    s.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                }
                else
                {
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
                }
                
                x++;
                //f.z.RemoveAt(0);
            }
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + "qual complete" + "\n";

                }
            }

            /*
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
            */
        }
      


    }
}
