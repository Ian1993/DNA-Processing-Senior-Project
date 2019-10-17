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
            Filemanager f;
            f = new Filemanager();
            one = f.trimmerselector();
            blockreader(titleline, SequenceLine, QualityLine, one);
            TrimmerOneFile();
        }

        public void twofile()
        {
            Filemanager x;
            Filemanager y;
            x = new Filemanager();
            y = new Filemanager();

            one = x.trimmerselector();
            two = y.trimmerselector();
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

        }

        public void TrimmerOneFile()
        {

        }

        public void TrimmerTwoFIle()
        {

        }
    }
}
