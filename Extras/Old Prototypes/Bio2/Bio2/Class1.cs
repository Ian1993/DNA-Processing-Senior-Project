using System.Windows;
using System.Diagnostics;
using System.IO;
using System;
using System.Collections;

namespace Bio2
{
    class FilemanagerFastq
    {
        //private System.IO.StreamReader file;
        private string direct;
        private char[][] sequ = new char[8192][];
        private char[][] qual = new char[8192][];

        private string[][] sizes = new string[8192][];
        private string[][] seqnames = new string[8192][];
        string temp;
        private StreamReader fs;
        private int k = 64;
        private int y = 49;
        Hashtable qualityaccepted = new Hashtable();
        Hashtable qualityrejected = new Hashtable();
        int windowqual = 50;
        int minqual = 50;
        int winsize = 20;
        int readavg = 50;
        int regwin = 2;
        int winavg;
        int rdavg;
        int regwincount = 0;
        public FilemanagerFastq()
        {
            
        }
        public void driver()
        {
            //Performs all function calls for FilemanagerFastq class

            /*
            while (fs.EndOfStream != true)
            {
                fileopener();
                print();
                sequ = new char[512][];
                qual = new char[512][];                                                                                                                                                                                                       

                sizes = new string[512][];
                seqnames = new string[512][];
             }
             */
            fileopener();

            qualitytrm();

            print();


        }
        /*
        private void filereaderloopfirstpass()
        {
            fs.Read();
        }
        */
        public void fileselectordialg()
        {
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

        }

        public void graphavgdriver()
        {
            fileselectordialg();
            fileopener();
            for(int x = 0; x<k; x++)
            {

            }

        }

        private void hashchecker(int x)
        {
            if(rdavg>=readavg && regwincount<=regwin){
                //if the readaverage is not at least the prescribed amount and the number of rejected windows is not less than prescribed, reject
                try
                {
                    qualityaccepted.Add(seqnames[x][0], sequ[x]);
                }
                catch
                {

                }
                
            }
            else
            {
                try
                {
                    qualityrejected.Add(seqnames[x][0], sequ[x]);
                    qualityaccepted.Add(seqnames[x][0], "Rejected");
                }
                catch
                {

                }
            }
        }
        private void qualitytrm()
        {
            int z;
            
            int j = 0;
            int p = 0;
            int t = 0;
            for (int i = 0; i<k; i++)
            {  
                //determines row of array being used
                winavg = 0;
                rdavg = 0;
                regwincount = 0;
                while( j < (Convert.ToInt32(sizes[i][1])))
                {
                    //Window loop
                    for(int x = p; x < j; x++)
                    {
                        z = Convert.ToInt32(qual[i][j]);
                        if (z >= minqual)
                        {   
                            //check point for quality, if true, add value to average
                            winavg = winavg + z;
                        }
                        else
                        {
                            //if false, replace element with white space
                            sequ[i][j] = ' ';
                        }
                        //use x as starting point for next window
                        t = x;
                    }
                    p = t;

                    //increment j to next window
                    j = j + winsize;

                    //calculate window average
                    winavg = winavg / 512;
                    if (winavg < windowqual)
                    {
                        //increment rejected window count up
                        regwincount = regwincount + 1;
                    }

                }
                p = 0;

                

                hashchecker(i);
            }
        }
        private void print()
        {
            for (int j = 0; j < k; j++)
            {
                if (seqnames[j][0] != null)
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        /*
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).Statusbox.Text = (window as MainWindow).Statusbox.Text + "\n" + seqnames[j][0] + " " + sizes[j][1] + "\n";

                        }
                        */
                        foreach (DictionaryEntry entry in qualityaccepted)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                //string set = ("{1}, {0}", entry.Key, entry.Value);
                                (window as MainWindow).Statusbox.Text = (window as MainWindow).Statusbox.Text + "\n" + entry.Key + entry.Value + "\n";

                            }
                        }
                    }

                }
                
            }
            
        }

        public void fileopener()
        {

           
            
            
            for(int i = 0; i<k; i++)
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
                temp = fs.ReadLine();

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
                fs.Read(sequ[i], 0, x);

                //read rest of line
                fs.ReadLine();

                //read rest of line
                fs.ReadLine();

                //read quality
                fs.Read(qual[i], 0, x);

                //read rest of line
                fs.ReadLine();
                

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
            
            foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        
                    (window as MainWindow).Statusbox.Text = (window as MainWindow).Statusbox.Text + "Reading Complete"  + "\n";
                }
                }
           
            

             
        }

        

        
    }
}
