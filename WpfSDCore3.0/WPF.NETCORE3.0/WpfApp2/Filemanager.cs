using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfApp2
{
    class Filemanager
    {
        public List<int> z = new List<int>();

        public object f1 { get; private set; }

        //public int si;

        public StreamReader fileselectordialg()
        {

            FileStream fl;
            Byte[] first = new Byte[512];
            Byte[] last = new Byte[512];
            string[] temp = new string[8192];
            string[] fnum = new string[8192];
            //int fsize;

            string[] lnum = new string[8192];



            StreamReader fs;
            string direct = null;
            //Opens file selection dialog, inputs it into string Direct, and opens fs streamreader with direct
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Fastq Files (*.Fastq)|*.Fastq";

            System.Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                direct = dlg.FileName;
            }
            try
            {
                fl = File.OpenRead(direct);
            }
            catch
            {
                throw ArgumentNullException(f1);
            }

            fl.Read(first, 0, 300);

            temp = Encoding.UTF8.GetString(first, 0, first.Length).Split('=', '\n');
            lnum = Encoding.UTF8.GetString(first, 0, first.Length).Split('.', ' ');

            //split name line to find name of sequence
            //seqnames[i] = temp.Split(' ');

            //make int x equal the length of sequence
            int x = Convert.ToInt32(temp[1]);





            fl.Seek(-2 * x, SeekOrigin.End);
            fl.Read(last, 0, 300);
            fl.Close();
            fnum = Encoding.UTF8.GetString(last, 0, last.Length).Split('.', ' ');


            Random y = new Random();
            int q;
            for (int i = 0; i < .05 * (Convert.ToInt32(fnum[2]) - Convert.ToInt32(lnum[2])); i++)
            {



                q = y.Next(Convert.ToInt32(lnum[2]), Convert.ToInt32(fnum[2]));
                //Predicate<int> t = q;
                if (z.IndexOf(q) == -1)
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

        private Exception ArgumentNullException(object f1)
        {
            throw new ArgumentNullException();
        }
    }
}
