using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolVis
{
    class Bond
    {
        int bondWith; // Номер атома с которым связан владелец
        Boolean isPainted; // Показывает отрисована ли уже эта связь

        public Bond()
        {
            bondWith = -1;
            isPainted = false;
        }

        public Bond(int number)
        {
            bondWith = number;
            isPainted = false;
        }

        public Boolean IsPainted
        {
            get { return isPainted; }
            set { isPainted = value; }
        }

        public int BondWith
        {
            get { return bondWith; }
            set { bondWith = value; }
        }
    }
}
