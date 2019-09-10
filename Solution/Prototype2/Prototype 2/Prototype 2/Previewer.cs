using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace windows
{
    class Previewer
    {
        private int winsize = 500;

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
            Filemanager f = new Filemanager();
            StreamReader s = f.fileselectordialg();
        }
    }
}
