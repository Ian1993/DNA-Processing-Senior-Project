using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windows;

namespace Prototype_2
{
    class Trimmer
    {

        private int window = 50;
        private int minwin = 50;
        private int minqual= 50;
        private int blocksize = 500;
        private StreamReader one;
        private StreamReader two;
        private Filemanager f;
        private Filemanager x;
        private Filemanager y;
        string directa;
        string directb;

        private StreamWriter onea;
        private StreamWriter twoa;

        private List<string> titleline;
        private List<string> SequenceLine;
        private List<string> QualityLine;

        private List<string> titleline2;
        private List<string> SequenceLine2;
        private List<string> QualityLine2;

        public void SettingsStarter(int x, int y, int z)
        {
            window = x;
            minwin = y;
            minqual = z;

        }

        public void singlefile()
        {
            
            f = new Filemanager();
            one = f.trimmerselector();
            directa = f.directgetter();
            onea = new StreamWriter(directa);
            blockreader(titleline, SequenceLine, QualityLine, one);
            TrimmerOneFile();
            
        }

        public void twofile()
        {
            
            x = new Filemanager();
            y = new Filemanager();

            one = x.trimmerselector();
            two = y.trimmerselector();

            directa = x.directgetter();
            directb = y.directgetter();
            onea = new StreamWriter(directa);
            twoa = new StreamWriter(directb);
            blockreader(titleline, SequenceLine, QualityLine, one);
            blockreader(titleline2, SequenceLine2, QualityLine2, two);
            TrimmerTwoFIle();
        }

        public void blockreader(List<string> title, List<string> seq, List<string> qual, StreamReader a)
        {
            for(int k = 0; k<blocksize; k++)
            {
                title.Add(a.ReadLine());
                seq.Add(a.ReadLine());
                a.ReadLine();
                qual.Add(a.ReadLine());
            }
        }

        public void filesaver(List<string> title, List<string> seq, List<string> qual, StreamWriter b)
        {
            while (qual != null)
            {
                b.WriteLine(title[0]);
                title.RemoveAt(0);
                b.WriteLine(seq[0]);
                seq.RemoveAt(0);
                b.WriteLine("+");
                b.WriteLine(qual[0]);
                qual.RemoveAt(0);
            }
        }

        public void TrimmerOneFile()
        {
            char[] set = null;
            char[] qul = null;
            int z = 0;
            int average = 0;
            int windowaverage = 0;

            for(int x = 0; x < titleline.Count; x++)
            {
                set = SequenceLine[z].ToCharArray();
                qul = QualityLine[z].ToCharArray();
                Boolean acceptwindows = true;
                Boolean acceptaverage = true;
                for(int y = 0; y < set.Length; y++)
                {

                    windowaverage = windowaverage + Convert.ToInt32(qul[y]);

                    if (Convert.ToInt32(qul[y]) < minqual)
                    {
                        qul[y] = Convert.ToChar(0);
                        set[y] = 'n';
                    }
                    if(y!=0 && (window % y) == 0)
                    {
                        average = windowaverage;
                        windowaverage = windowaverage / window;
                        
                        if (average < minwin)
                        {
                            titleline.RemoveAt(z);
                            SequenceLine.RemoveAt(z);
                            QualityLine.RemoveAt(z);
                            z = z - 1;
                            acceptwindows = false;
                            break;
                        }

                    }

                   

                }
                average = average / set.Length;
                if (average < minqual)
                {
                    acceptaverage = true;
                }
                if(acceptwindows == true && acceptaverage == true)
                {
                    string t = Convert.ToString(qul);
                    string u = Convert.ToString(set);
                    SequenceLine[z] = u;
                    QualityLine[z] = t;
                }
                z++;
            }


        }

        public void TrimmerTwoFIle()
        {

        }
    }
}
