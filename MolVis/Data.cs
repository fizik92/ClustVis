using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MolVis
{
    static class Data
    {
        static int n; //количество атомов в молекуле
        static int nstart, nfinish, nalpha; //с какого по какой красим, прозрачность
        static Color col; //цвет
        static Boolean done; //нажата клавиша ок, форма закрыта

        static Boolean Brelaxing, BstopThread;

        static float radiusA, radiusB;

        static Data()
        {
            n = 0;
            nstart = -1;
            nfinish = -1;
            nalpha = -1;
            col = Color.Black;
            BRelaxing = false;
            BstopThread = false;
            radiusA = 0.2f;
            radiusB = 0.1f;
        }

        static public int N
        {
            get { return n; }
            set { n = value; }
        }

        static public int Nstart
        {
            get { return nstart; }
            set { nstart = value; }
        }

        static public int Nfinish
        {
            get { return nfinish; }
            set { nfinish = value; }
        }

        static public int Nalpha
        {
            get { return nalpha; }
            set { nalpha = value; }
        }

        static public Color Ncolor
        {
            get { return col; }
            set { col = value; }
        }

        static public Boolean Done
        {
            get { return done; }
            set { done = value; }
        }

        static public Boolean BRelaxing
        {
            get { return Brelaxing; }
            set { Brelaxing = value; }
        }

        static public Boolean BStopThread
        {
            get { return BstopThread; }
            set { BstopThread = value; }
        }

        static public float RadiusA
        {
            get { return radiusA; }
            set { radiusA = value; }
        }

        static public float RadiusB
        {
            get { return radiusB; }
            set { radiusB = value; }
        }
    }
}
