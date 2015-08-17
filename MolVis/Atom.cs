using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolVis
{
    class Atom
    {
        Vector coordinates;
        string name;
        int n;
        List<Bond> bonds; // связи
        Vector atomColor; // цвет атома
        int atomTrans; // прозрачность

        public Atom(string line)
        {
            string s = line;
            s = removeSpaces(s);
            s = s.TrimStart(' ');
            s = s.TrimEnd(' ');
            s = s.TrimEnd('\r');
            string[] strs = s.Split(' ');
            coordinates = new Vector(float.Parse(strs[0].Replace('.', ',')), 
                float.Parse(strs[1].Replace('.', ',')), 
                float.Parse(strs[2].Replace('.', ',')));
            name = strs[3];
            n = getNumberOfAtom(strs[3]);
            bonds = new List<Bond>(getNumberOfBonds(name));
            for (int i = 0; i < getNumberOfBonds(name); i++)
            {
                bonds.Add(new Bond());
            }
            getColorOfAtom();
            atomTrans = 255;
        }

        public Atom(Vector v, string s)
        {
            coordinates = new Vector(v.X, v.Y, v.Z);
            name = s;
            n = getNumberOfAtom(s);
            bonds = new List<Bond>(getNumberOfBonds(s));
            for (int i = 0; i < getNumberOfBonds(name); i++)
            {
                bonds.Add(new Bond());
            }
            getColorOfAtom();
            atomTrans = 255;
        }

        public Atom(int ax, int ay, int az, string aname)
        {
            coordinates = new Vector(ax, ay, az);
            name = aname;
            n = getNumberOfAtom(aname);
            bonds = new List<Bond>(getNumberOfBonds(aname));
            for (int i = 0; i < getNumberOfBonds(name); i++)
            {
                bonds.Add(new Bond());
            }
            getColorOfAtom();
            atomTrans = 255;
        }

        private string removeSpaces(string s)
        {
            string str = s.Replace("  ", " ");
            if (str.Contains("  "))
            {
                str = removeSpaces(str);
            }
            return str;
        }

        private int getNumberOfAtom(string s)
        {
            int number = 0;
            switch (name)
            {
                case "H": number = 1; break;
                case "C": number = 6; break;
                case "O": number = 8; break;
                case "N": number = 7; break;
                default: number = 0; break;
            }
            return number;
        }

        private int getNumberOfBonds(string s)
        {
            int number = 0;
            switch (name)
            {
                case "H": number = 1; break;
                case "C": number = 4; break;
                case "O": number = 6; break;
                case "N": number = 5; break;
                default: number = 0; break;
            }
            return number;
        }

        private void getColorOfAtom()
        {
            switch (name)
            {
                case "H": atomColor = new Vector(0.5f, 0.9f, 1.0f); break; //голубой
                case "C": atomColor = new Vector(0.2f, 0.2f, 0.2f); break; //чёрный
                case "O": atomColor = new Vector(1.0f, 0.0f, 0.0f); break; //красный
                case "N": atomColor = new Vector(0.0f, 0.8f, 0.0f); break; //зелёный
                default: atomColor = new Vector(0.0f, 0.0f, 1.0f); break;
            }
        }

        public float X
        {
            get { return coordinates.X; }
            set { coordinates.X = value; }
        }

        public float Y
        {
            get { return coordinates.Y; }
            set { coordinates.Y = value; }
        }

        public float Z
        {
            get { return coordinates.Z; }
            set { coordinates.Z = value; }
        }

        public string NAME
        {
            get { return name; }
        }

        public int N
        {
            get { return n; }
        }

        public Vector Coordinates
        {
            get { return coordinates; }
            set
            {
                coordinates.X = value.X;
                coordinates.Y = value.Y;
                coordinates.Z = value.Z;
            }
        }

        public string getString()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            return "   " + String.Format(culture, "{0: 0.0000000;#-0.0000000}", coordinates.X)
                + "      " + String.Format(culture, "{0: 0.0000000;#-0.0000000}", coordinates.Y)
                + "      " + String.Format(culture, "{0: 0.0000000;#-0.0000000}", coordinates.Z)
                + "      " + name;
        }

        public Vector Color
        {
            get
            {
                return atomColor;
            }
            set
            {
                atomColor = value;
            }
        }

        public int Transparency
        {
            get { return atomTrans; }
            set { atomTrans = value; }
        }

        public List<Bond> Bonds
        {
            get { return bonds; }
        }

        // Возвращает true, если все связи заняты
        public Boolean BondsIsBusy
        {
            get
            {
                Boolean bondsisbusy = true;
                for (int i = 0; i < bonds.Count; i++)
                {
                    if (bonds[i].BondWith < 0) bondsisbusy = false;
                }
                return bondsisbusy;
            }
        }

        // Возвращает номер произвольной свобоной связи в массиве
        public int FreeBond
        {
            get
            {
                int i = 0;
                while (bonds[i].BondWith > -1)
                {
                    i++;
                }
                return i;
            }
        }

        // Возвращает количество свободных связей у атома
        public int numberOfFreeBonds
        {
            get
            {
                int num = 0;
                for (int i = 0; i < bonds.Count; i++)
                {
                    if (bonds[i].BondWith < 0) num++;
                }
                return num;
            }
        }

        // Возвращает true, если в списке связей уже есть такая связь
        public Boolean alreadyExists(int min)
        {
            for (int i = 0; i < bonds.Count; i++)
            {
                if (bonds[i].BondWith == min) return true;
            }
            return false;
        }
    }
}
