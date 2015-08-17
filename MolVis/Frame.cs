using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolVis
{
    class Frame
    {
        List<Vector> atoms;
        int k; //общее число атомов

        public Frame()
        {
            atoms = new List<Vector>();
            k = 0;
        }

        public Frame(string[] lines)
        {
            if (lines[lines.Length - 1] != "") k = lines.Length;
            else k = lines.Length - 1;
            atoms = new List<Vector>(k);
            for (int i = 0; i < k; i++)
            {
                atoms.Add(getCoordinates(lines[i]));
            }

        }

        private Vector getCoordinates(string line)
        {
            string s = line;
            s = removeSpaces(s);
            s = s.TrimStart(' ');
            s = s.TrimEnd(' ');
            s = s.TrimEnd('\r');
            string[] strs = s.Split(' ');
            return new Vector(float.Parse(strs[0].Replace('.', ',')),
                float.Parse(strs[1].Replace('.', ',')),
                float.Parse(strs[2].Replace('.', ',')));
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

        public float getX(int i)
        {
            return atoms[i].X;
        }

        public float getY(int i)
        {
            return atoms[i].Y;
        }

        public float getZ(int i)
        {
            return atoms[i].Z;
        }
    }
}
