using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Windows;

namespace windows
{
    class Filemanager
    {
        public List<int> z = new List<int>();
        string direct = null;
        //public int si;

        public StreamReader trimmerselector()
        {
            StreamReader fs;
            
            //Opens file selection dialog, inputs it into string Direct, and opens fs streamreader with direct
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Fastq Files (*.Fastq)|*.Fastq";

            System.Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                direct = dlg.FileName;
            }
            fs = new StreamReader(direct);

            return fs;
        }

        public StreamReader fileselectordialg()
        {

            FileStream fl;
            Byte[] first = new Byte[512];
            Byte[] last = new Byte[512];
            string[] temp= new string[8192];
            string[] fnum= new string[8192];
            //int fsize;

            string[] lnum = new string[8192];
            
          
            
            StreamReader fs;
            //string direct = null;
            //Opens file selection dialog, inputs it into string Direct, and opens fs streamreader with direct
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Fastq Files (*.Fastq)|*.Fastq";

            System.Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                direct = dlg.FileName;
            }
            fl = File.OpenRead(direct);
            fl.Read(first, 0, 300);

            temp = Encoding.UTF8.GetString(first, 0, first.Length).Split('=','\n');
            lnum = Encoding.UTF8.GetString(first, 0, first.Length).Split('.', ' ');

            //split name line to find name of sequence
            //seqnames[i] = temp.Split(' ');

            //make int x equal the length of sequence
            int x = Convert.ToInt32(temp[1]);



                    

            fl.Seek(-2*x, SeekOrigin.End);
            fl.Read(last,0, 300);
            fl.Close();
            fnum = Encoding.UTF8.GetString(last, 0, last.Length).Split('.', ' ');


            Random y = new Random();
            int q;
            for (int i = 0; i < .05*(Convert.ToInt32(fnum[2]) - Convert.ToInt32(lnum[2]));i++){
                


                q = y.Next(Convert.ToInt32(lnum[2]), Convert.ToInt32(fnum[2]));
                //Predicate<int> t = q;
                if (z.IndexOf(q)==-1)
                {
                    z.Add(q);
                }

            }
            


            /*
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Window1))
                {
                    (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + x + "\n" + lnum[2] + "\n" + fnum[2] +  "\n";

                }
            }
            
            foreach(int i in z)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Window1))
                    {
                        (window as Window1).StatusBox.Text = (window as Window1).StatusBox.Text + "\n" + i + "\n";

                    }
                }
            }
            */
            
            fs = new StreamReader(direct);
            z.Sort();

            return fs;

        }

        public string directgetter()
        {
            string direct2 = direct.Substring(0, (direct.Length) - 5);
            direct2 = direct2 + "_trimmed.fastq";
            return direct2;
        }
    }
}
