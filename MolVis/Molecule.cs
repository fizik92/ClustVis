using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MolVis
{
    class Molecule
    {
        List<Atom> atoms;
        int k; //общее число атомов
        int first, second, third, forth; //Для выделения атомов
        int rightChecked;

        public Molecule()
        {
            atoms = new List<Atom>();
            k = 0;
            first = -1;
            second = -1;
            third = -1;
            forth = -1;
            rightChecked = -1;
        }

        public Molecule(string[] lines)
        {
            if (lines[lines.Length - 1] != "") k = lines.Length;
            else k = lines.Length - 1;
            atoms = new List<Atom>(k);
            for (int i = 0; i < k; i++)
            {
                atoms.Add(new Atom(lines[i]));
            }
            first = -1;
            second = -1;
            third = -1;
            forth = -1;
            rightChecked = -1;
        }

        public string[] getString()
        {
            string[] lines = new string[k];
            for (int i = 0; i < k; i++)
            {
                lines[i] = atoms[i].getString();
            }
            return lines;
        }

        public Atom getAtom(int l)
        {
            return atoms[l];
        }

        // Количество атомов в молекуле
        public int K
        {
            get { return k; }
        }

        // "Диаметр молекулы"
        public float Diameter
        {
            get
            {
                float maxR = 0;
                Vector zero = new Vector();
                float distance = 0;
                for (int i = 0; i < k; i++)
                {
                    distance = zero & atoms[i].Coordinates;
                    if (distance > maxR) maxR = distance;
                }
                return 2 * maxR;
            }
        }

        // "Центр молекулы" - мат ожидание координат всех атомов
        public Vector Center
        {
            get
            {
                Vector center = new Vector();
                for (int i = 0; i < k; i++)
                {
                    center.X += atoms[i].X;
                    center.Y += atoms[i].Y;
                    center.Z += atoms[i].Z;
                }
                center.X /= k;
                center.Y /= k;
                center.Z /= k;
                return center;
            }
        }

        // Добавление атома в молекулу
        public void addAtom(string atomName, Vector newatom)
        {
            atoms.Add(new Atom(newatom, atomName));
            k++;
        }

        // Удаление атома из молекулы
        public void deleteAtom(int l)
        {
            first = -1;
            second = -1;
            third = -1;
            forth = -1;
            atoms.RemoveAt(l);
            k--;
            setBonds();
        }

        // Рассчитывает какие атомы связаны между собой связями во всей молекуле
        public void setBonds()
        {
            clearBonds();
            resetBonds();
            for (int q = 1; q < 9; q++)
            {
                for (int i = 0; i < k; i++)
                {
                    if (atoms[i].Bonds.Count == q)
                    {
                        List<int> mas = new List<int>(k);
                        for (int j = 0; j < k; j++)
                        {
                            if (i != j && (atoms[i].Coordinates & atoms[j].Coordinates) < 1.8)
                            {
                                mas.Add(j);
                            }
                        }

                        /*
                        int numfb = atoms[i].numberOfFreeBonds;
                        int p = 0;
                        while(p < numfb && mas.Count != 0)
                        {
                            if (mas.Count != 0)
                            {
                                int fmin = findMin(mas, i);
                                if (!atoms[mas[fmin]].BondsIsBusy)
                                {
                                    atoms[i].Bonds[atoms[i].FreeBond].BondWith = mas[fmin];
                                    atoms[mas[fmin]].Bonds[atoms[mas[fmin]].FreeBond].BondWith = i;
                                    p++;
                                }
                                mas.RemoveAt(fmin);
                            }
                        }
                         */
                        while (mas.Count != 0)
                        {
                            if (atoms[i].numberOfFreeBonds == 0)
                            {
                                mas.Clear();
                            }
                            else
                            {
                                int fmin = findMin(mas, i);

                                // Если у атома, до которого минимальное расстояние 
                                // нет свободных связей, то этот атом не подходит
                                // ИЛИ в списке связей текущего атома такая связь уже есть
                                if (!atoms[mas[fmin]].BondsIsBusy && !atoms[i].alreadyExists(mas[fmin]))
                                {
                                    atoms[i].Bonds[atoms[i].FreeBond].BondWith = mas[fmin];
                                    atoms[mas[fmin]].Bonds[atoms[mas[fmin]].FreeBond].BondWith = i;
                                }
                                mas.RemoveAt(fmin);
                            }
                        }
                    }
                }
            }
        }

        // Стирает все связи у всех атомов
        private void clearBonds()
        {
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < atoms[i].Bonds.Count; j++)
                {
                    atoms[i].Bonds[j].BondWith = -1;
                }
            }
        }

        // Ставит все связи у всех атомов в состояние неотрисованности
        public void resetBonds()
        {
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < atoms[i].Bonds.Count; j++)
                {
                    atoms[i].Bonds[j].IsPainted = false;
                }
            }
        }

        private int findMin(List<int> m, int i)
        {
            int min = 0;
            for (int l = 1; l < m.Count; l++)
            {
                if ((atoms[i].Coordinates & atoms[m[min]].Coordinates)
                    > (atoms[i].Coordinates & atoms[m[l]].Coordinates))
                {
                    min = l;
                }
            }
            return min;
        }

        public int First
        {
            set { first = value; }
            get { return first; }
        }

        public int Second
        {
            set { second = value; }
            get { return second; }
        }

        public int Third
        {
            set { third = value; }
            get { return third; }
        }

        public int Forth
        {
            set { forth = value; }
            get { return forth; }
        }

        public int RightChecked
        {
            set { rightChecked = value; }
            get { return rightChecked; }
        }

        public int numberChecked
        {
            get
            {
                if (forth >= 0) return 4;
                else
                {
                    if (third >= 0) return 3;
                    else
                    {
                        if (second >= 0) return 2;
                        else
                        {
                            if (first >= 0) return 1;
                        }
                    }
                }
                return 0;
            }
        }

        public void resetCheck()
        {
            first = -1;
            second = -1;
            third = -1;
            forth = -1;
        }

        public Boolean alreadyChecked(int number)
        {
            return (number == first || number == second
                || number == third || number == forth);
        }

        // Функция копирования (считаем, что рассматривается та же самая молекула)
        // Значит копировать, т е изменzть, нужно только координаты атомов
        // Эта функция нужна для воспроизведения фильмов
        public void copy(Molecule mol)
        {
            for (int i = 0; i < k; i++)
            {
                atoms[i].Coordinates.X = mol.getAtom(i).Coordinates.X;
                atoms[i].Coordinates.Y = mol.getAtom(i).Coordinates.Y;
                atoms[i].Coordinates.Z = mol.getAtom(i).Coordinates.Z;
            }
        }

        public void copy(Frame frame)
        {
            for (int i = 0; i < k; i++)
            {
                atoms[i].Coordinates.X = frame.getX(i);
                atoms[i].Coordinates.Y = frame.getY(i);
                atoms[i].Coordinates.Z = frame.getZ(i);
            }
        }
    }
}
