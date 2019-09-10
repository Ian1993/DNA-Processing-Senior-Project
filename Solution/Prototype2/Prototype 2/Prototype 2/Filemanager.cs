using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace windows
{
    class Filemanager
    {

        public StreamReader fileselectordialg()
        {
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
            fs = new StreamReader(direct);

            return fs;

        }
    }
}
