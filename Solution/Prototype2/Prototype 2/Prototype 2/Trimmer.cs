using System;
using System.IO;
using windows;

public class Trimmer
{
    private int winsize = 500;
    private StreamReader s;
    //private string direct;
    private char[][] sequ = new char[8192][];
    private char[][] qual = new char[8192][];

    private string[][] sizes = new string[8192][];
    private string[][] seqnames = new string[8192][];
    string temp;
    Filemanager f;


    public Trimmer()
	{
	}
    public void fileselector()
    {
        f = new Filemanager();
        s = f.fileselectordialg();

    }

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
