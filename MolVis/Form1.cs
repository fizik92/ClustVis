using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;


namespace MolVis
{
    public partial class Form1 : Form
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        unsafe public delegate void CallBack(double* masx, double* masy, double* masz, ref int nm);

        [DllImport("srelax.dll", EntryPoint = "_MAIN@24",
            CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern void smain(double[] masx, double[] masy,
            double[] masz, int[] mast, ref int masn, CallBack cb);

        /*
        [DllImport("First_fortran_dll.dll", EntryPoint = "_TESTDELEGATE@20",
            CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void testDelegate(ref int masn, double[] masx, double[] masy,
            double[] masz, CallBack cb);
         * */

        //******************************************************
        Molecule molecule;
        Frame[] frames;
        float radius;  // радиус атома
        float radiusBond; //радиус связи
        float viewSize; // размер экранных координат
        float scale; // масштаб
        int mouseX, mouseY; // текущие координаты мыши
        int atomCheckedMove, atomCheckedDelete;
        Boolean BmouseIsDown; 
        Boolean BMoveRegime;
        Boolean BNumbersIsChecked;
        Quaternion q;// кватернион глобальный
        Vector current; //Нужен для того, чтобы не перемещать атом во время выделения
        Boolean BEditRegime; //Равен тру, если подсвечена кнопка Edit меню
        String nameOfNewAtom; // Задаёт тип добавляемого атома
        int frameN; // Нужно для фильмов, задаёт номер отображаемого кадра
        Boolean BMovie; // Показывает, что сейчас идёт работа с фильмом или с молекулой
        Boolean BMovieManager; // Показывает отображается ли панель для работы с фильмами

        Thread relaxing; //Для запуска релаксации в отдельном потоке

        //******************************************************

        public Form1()
        {
            InitializeComponent();
            ant.InitializeContexts();
            ant.MouseWheel += new MouseEventHandler(ant_MouseWheel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            molecule = new Molecule();
            initialize();
            BMovie = false;
            BMovieManager = false;

            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Gl.glClearColor(255, 255, 255, 0); //
            tune();

            Gl.glEnable(Gl.GL_NORMALIZE);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            float[] pos = { 20, 20, 100, 1 };
            float[] dir = { 1, 1, 1 };
            float[] mat_specular = { 1, 1, 1, 1 };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, pos);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, dir);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, mat_specular);
            Gl.glMaterialf(Gl.GL_FRONT, Gl.GL_SHININESS, 128.0f);

            /*
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glAlphaFunc(Gl.GL_ALWAYS, 0.5f); //???
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
             */

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            paint();
        }

        private void initialize()
        {
            viewSize = 100f;
            radius = 0.24f;
            q = new Quaternion();
            BMoveRegime = false;
            BmouseIsDown = false;
            BNumbersIsChecked = false;
            atomCheckedMove = -1;
            atomCheckedDelete = -1;
            scale = 30f;
            textBox.Visible = false;
            current = new Vector();
            BEditRegime = false;
            rotateAndMoveToolStripMenuItem.Checked = true;
            hToolStripMenuItem.Checked = true;
            numbersToolStripMenuItem.Checked = false;
            nameOfNewAtom = "H";
            frameN = 0;
        }

        //Настройка проекции
        private void tune()
        {
            float r = viewSize; //То же, что и viewSize, только короткое название
            Gl.glViewport(0, 0, ant.Width, ant.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            if ((float)ant.Width <= (float)ant.Height)
            {
                Gl.glOrtho(-r, r, -r * (float)ant.Height / (float)ant.Width,
                    r * (float)ant.Height / (float)ant.Width, -r, r);
            }
            else
            {
                Gl.glOrtho(-r * (float)ant.Width / (float)ant.Height,
                    r * (float)ant.Width / (float)ant.Height, -r, r, -r, r);
            }
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        private void paint()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            paintInformation();
            paintSample();
            double alfa = 2 * Math.Acos(q.W);
            Gl.glRotatef((float)(alfa * 180 / 3.14), (float)(q.X / Math.Sin(alfa / 2)),
                (float)(q.Y / Math.Sin(alfa / 2)), (float)(q.Z / Math.Sin(alfa / 2)));
            Gl.glScalef(scale, scale, scale);

            paintBonds(); //Рисуем связи в виде цилиндров
            paintAtoms(); //Рисуем атомы в виде шариков

            Gl.glFlush();
            ant.Invalidate();
        }

        private void paintAtoms()
        {
            /*
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glAlphaFunc(Gl.GL_ALWAYS, 0.5f); //???
            Gl.glDisable(Gl.GL_DEPTH_TEST);
             * * */


            Vector displacement = new Vector(molecule.Center.X,
                molecule.Center.Y, molecule.Center.Z);
            for (int i = 0; i < molecule.K; i++)
            {
                if (molecule.getAtom(i).Transparency <255)
                {
                    Gl.glEnable(Gl.GL_ALPHA_TEST);
                    Gl.glAlphaFunc(Gl.GL_ALWAYS, 0.5f); //???
                    Gl.glEnable(Gl.GL_BLEND);
                    Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
                    //Gl.glDisable(Gl.GL_DEPTH_TEST);
                }

                Gl.glPushMatrix();
                if (i == atomCheckedMove)
                {
                    Gl.glColor3f(1.0f, 0.6f, 0.9f);
                }
                else if (i == atomCheckedDelete)
                {
                    Gl.glColor3f(1.0f, 0.3f, 0.8f);
                }
                else if (i == molecule.First || i == molecule.Third)
                {
                    Gl.glColor3f(0.9f, 1.0f, 0.3f);
                }
                else if (i == molecule.Second || i == molecule.Forth)
                {
                    Gl.glColor3f(0.9f, 0.7f, 0.2f);
                }
                else
                {
                   // Gl.glColor3f(molecule.getAtom(i).Color.X,
                   //     molecule.getAtom(i).Color.Y, molecule.getAtom(i).Color.Z);
                    Gl.glColor4f(molecule.getAtom(i).Color.X,
                        molecule.getAtom(i).Color.Y, molecule.getAtom(i).Color.Z,
                        molecule.getAtom(i).Transparency);
                }
                Gl.glTranslatef(molecule.getAtom(i).X - displacement.X,
                    molecule.getAtom(i).Y - displacement.Y, 
                    molecule.getAtom(i).Z - displacement.Z);
                Glut.glutSolidSphere(radius, 20, 20);
                if (BNumbersIsChecked) paintNumbers(i);
                Gl.glPopMatrix();
                if (molecule.getAtom(i).Transparency < 255)
                {
                    Gl.glDisable(Gl.GL_BLEND);
                    Gl.glDisable(Gl.GL_ALPHA_TEST);
                    //Gl.glEnable(Gl.GL_DEPTH_TEST);
                }
            }

            /*
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glDisable(Gl.GL_BLEND);
            Gl.glDisable(Gl.GL_ALPHA_TEST);
             * */
        }

        private void paintBonds()
        {
            Vector disp = new Vector(molecule.Center.X, 
                molecule.Center.Y, molecule.Center.Z); // displacement - смещение молекулы от центра
            for (int i = 0; i < molecule.K; i++)
            {
                foreach (Bond bond in molecule.getAtom(i).Bonds)
                {
                    if (!bond.IsPainted && bond.BondWith > -1)
                    {
                        Vector vi = new Vector(molecule.getAtom(i).X,
                            molecule.getAtom(i).Y, molecule.getAtom(i).Z);
                        Vector vj = new Vector(molecule.getAtom(bond.BondWith).X,
                            molecule.getAtom(bond.BondWith).Y, molecule.getAtom(bond.BondWith).Z);
                        Vector v2 = new Vector(vj.X - vi.X,
                            vj.Y - vi.Y,  vj.Z - vi.Z );
                        Vector v1 = new Vector(0, 0, vi & vj);
                        Quaternion p = v1 | v2;
                        
                        /*
                        if (molecule.getAtom(i).Transparency < 255)
                        {
                            Gl.glEnable(Gl.GL_ALPHA_TEST);
                            Gl.glAlphaFunc(Gl.GL_ALWAYS, 0.5f); //???
                            Gl.glEnable(Gl.GL_BLEND);
                            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
                            Gl.glDisable(Gl.GL_DEPTH_TEST);
                        }
                         * */
                        Gl.glPushMatrix();
                        Gl.glColor4f(0.85f, 0.85f, 0.85f, 0.3f);
                        Gl.glTranslatef(vi.X - disp.X, 
                            vi.Y - disp.Y, vi.Z - disp.Z);
                        double alfa = 2 * Math.Acos(p.W);
                        //Gl.glRotatef((float)(a * 180 / 3.14), cp.X, cp.Y, cp.Z);
                        Gl.glRotatef((float)(alfa * 180 / 3.14), (float)(p.X / Math.Sin(alfa / 2)),
                            (float)(p.Y / Math.Sin(alfa / 2)), (float)(p.Z / Math.Sin(alfa / 2)));
                        Glut.glutSolidCylinder(0.1f, vi & vj, 20, 20);
                        Gl.glPopMatrix();
                        /*
                        if (molecule.getAtom(i).Transparency < 255)
                        {
                            Gl.glDisable(Gl.GL_BLEND);
                            Gl.glDisable(Gl.GL_ALPHA_TEST);
                            Gl.glEnable(Gl.GL_DEPTH_TEST);
                        }
                        */

                        //Теперь помечаем эту связь в обоих атомах
                        //как уже отрисованную
                        bond.IsPainted = true;
                        foreach (Bond bnd in molecule.getAtom(bond.BondWith).Bonds)
                        {
                            if (bnd.BondWith == i) bnd.IsPainted = true;
                        }
                    }
                }
            }
            molecule.resetBonds();
        }

        private void paintNumbers(int i)
        {
            Vector v1 = new Vector(-radius / 2, -radius / 2, radius + 0.05f);
            Vector v2 = v1 * !q;
            Gl.glDisable(Gl.GL_LIGHT0);
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            Gl.glRasterPos3d(v2.X, v2.Y, v2.Z);
            Glut.glutBitmapString(Glut.GLUT_BITMAP_TIMES_ROMAN_24, Convert.ToString(i));
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
        }

        //Отображение брусочка длиной в 1А
        private void paintSample()
        {
            float w, h;
            if ((float)ant.Width <= (float)ant.Height)
            {
                w = viewSize;
                h = viewSize * (float)ant.Height / (float)ant.Width;
            }
            else
            {
                w = viewSize * (float)ant.Width / (float)ant.Height;
                h = viewSize;
            }
            Vector sample = new Vector(ant.Width - 10, ant.Height - 20, 0);
            sample.X = (sample.X - ant.Width / 2) * 2 * w / (ant.Width * scale);
            sample.Y = (ant.Height / 2 - sample.Y) * 2 * h / (ant.Height * scale);
            sample.Z = 95 / scale;
            float rad = 10 * 2 * h / (ant.Height * scale);
            Gl.glPushMatrix();
            Gl.glScalef(scale, scale, scale);
            Gl.glTranslatef(sample.X, sample.Y, sample.Z);
            Gl.glRotatef(-90f, 0f, 1f, 0f);
            Gl.glColor3f(0.2f, 0.2f, 1.0f);
            Glut.glutSolidCylinder(rad, 1f, 20, 20);
            Gl.glPopMatrix();
        }

        private void paintInformation()
        {
            if (molecule.numberChecked == 1) paintInfo1();
            else if (molecule.numberChecked == 2) paintInfo2();
            else if (molecule.numberChecked == 3) paintInfo3();
            else if (molecule.numberChecked == 4) paintInfo4();
        }

        private void paintInfo1()
        {
            textBox.Visible = true;
            textBox.Text = "  #" + Convert.ToString(molecule.First)
                + sfrmt(molecule.getAtom(molecule.First).Coordinates);
        }

        private void paintInfo2()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            string s1 = "  #" + Convert.ToString(molecule.First)
                + sfrmt(molecule.getAtom(molecule.First).Coordinates);
            string s2 = "  #" + Convert.ToString(molecule.Second)
                + sfrmt(molecule.getAtom(molecule.Second).Coordinates);
            string s = "  distance = " + string.Format(culture, "{0:0.000}",
                molecule.getAtom(molecule.First).Coordinates &
                molecule.getAtom(molecule.Second).Coordinates);
            textBox.Text = s1 + "\r\n" + s2 + "\r\n" + s;
        }

        private void paintInfo3()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            string s1 = "  #" + Convert.ToString(molecule.First)
                + sfrmt(molecule.getAtom(molecule.First).Coordinates);
            string s2 = "  #" + Convert.ToString(molecule.Second)
                + sfrmt(molecule.getAtom(molecule.Second).Coordinates);
            string s3 = "  #" + Convert.ToString(molecule.Third)
                + sfrmt(molecule.getAtom(molecule.Third).Coordinates);
            string s = "  angle = " + string.Format(culture, "{0:0.00}",
                molecule.getAtom(molecule.First).Coordinates
                - molecule.getAtom(molecule.Second).Coordinates ^
                molecule.getAtom(molecule.Third).Coordinates
                - molecule.getAtom(molecule.Second).Coordinates); //angle(v1 - v2, v3 - v2)
            textBox.Text = s1 + "\r\n" + s2 + "\r\n" + s3 + "\r\n" + s;
        }

        private void paintInfo4()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            string s1 = "  #" + Convert.ToString(molecule.First)
                + sfrmt(molecule.getAtom(molecule.First).Coordinates);
            string s2 = "  #" + Convert.ToString(molecule.Second)
                + sfrmt(molecule.getAtom(molecule.Second).Coordinates);
            string s3 = "  #" + Convert.ToString(molecule.Third)
                + sfrmt(molecule.getAtom(molecule.Third).Coordinates);
            string s4 = "  #" + Convert.ToString(molecule.Forth)
                + sfrmt(molecule.getAtom(molecule.Forth).Coordinates);
            string s = "  angle = " + string.Format(culture, "{0:0.00}",
                molecule.getAtom(molecule.First).Coordinates 
                - molecule.getAtom(molecule.Second).Coordinates ^
                molecule.getAtom(molecule.Third).Coordinates
                - molecule.getAtom(molecule.Forth).Coordinates); //angle(v1 - v2, v3 - v4)
            textBox.Text = s1 + "\r\n" + s2 + "\r\n" + s3 + "\r\n" + s4 + "\r\n" + s;
        }

        //Вспомогательная функция для отображения информации в окошке
        private string sfrmt(Vector v)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            return "   x = " + string.Format(culture, "{0:0.000;#-0.000}", v.X)
                + "  y = " + string.Format(culture, "{0:0.000;#-0.000}", v.Y)
                + "  z = " + string.Format(culture, "{0:0.000;#-0.000}", v.Z);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (molecule != null)
            {
                tune();
                paint();
            }
        }

        // Возвращает тру, если открываемый файл является фильмом
        private Boolean isMovie(string s)
        {
            //формат:  1 kadr = 36 atomov
            if(s.IndexOf("=")>-1)
                return true;
            return false;
        }

        //Функция определяет сколько строчек в каждом кадре
        private int lengthOfFrame(string s)
        {
            //format:  1 kadr=          36 atomov
            string ss = removeSpaces(s);
            string[] str = ss.Split(' ');
            return int.Parse(str[3]);
        }

        // Функция удаляет все лишние пробелы
        private string removeSpaces(string s)
        {
            string str = s.Replace("  ", " ");
            if (str.Contains("  "))
            {
                str = removeSpaces(str);
            }
            return str;
        }

        private void openFile(StreamReader reader)
        {
            string text = reader.ReadToEnd();
            string[] lines = text.Split('\n');
            if (isMovie(lines[0])) //если это фильм (первая строчка: 1кадр= - 26 атомов)
            {
                int kadr = lengthOfFrame(lines[0]);
                MessageBox.Show("Открыт фильм!");
                string[] strmas = new string[kadr];
                frames = new Frame[(lines.Length - 2) / kadr];
                for (int i = 1, k = 0; k < (lines.Length - 2) / kadr; i = i + kadr, k++)
                {
                    for (int j = 0; j < kadr; j++)
                    {
                        strmas[j] = lines[i + j];
                    }
                    if (k == 0) molecule = new Molecule(strmas);
                    frames[k] = new Frame(strmas);
                }
                BMovie = true;
                addAndDeleteToolStripMenuItem.CheckOnClick = false;
                movieManagerToolStripMenuItem.CheckOnClick = true;
                movieManagerToolStripMenuItem.Checked = true;
                groupBox1.Visible = true;
                BMovieManager = true;
                trackBarFrame.SetRange(0, frames.Length - 1);
                trackBarSpeed.SetRange(20, 220);
                trackBarSpeed.Value = 100;
            }
            else //если это одна молекула
            {
                molecule = new Molecule(lines);
                BMovie = false;
                addAndDeleteToolStripMenuItem.CheckOnClick = true;
                movieManagerToolStripMenuItem.CheckOnClick = false;
                movieManagerToolStripMenuItem.Checked = false;
                rotateAndMoveToolStripMenuItem.Checked = true;
                groupBox1.Visible = false;
            }
            initialize();
            scale = viewSize / (1.2f * molecule.Diameter / 2);
            tune();
            molecule.setBonds();
            paint();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt) | *.txt; *.dat; | All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = ofd.OpenFile()) != null)
                {
                    using (StreamReader sr = new StreamReader(myStream))
                    {
                        openFile(sr);
                    }
                }
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (molecule != null)
            {
                String[] lines = molecule.getString();
                Stream myStream;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "txt files (*.txt) | *.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = sfd.OpenFile()) != null)
                    {
                        using(StreamWriter sw = new StreamWriter(myStream))
                        {
                            foreach (string line in lines)
                            {
                                sw.WriteLine(line);
                            }
                        }
                        //sw.Dispose();
                        myStream.Close();
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ant_MouseDown(object sender, MouseEventArgs e)
        {
            BmouseIsDown = true;
            if (!BEditRegime) // Если выбран режим вращения и перемещения
            {
                if (e.Button == MouseButtons.Right)
                {
                    int ina = inAtom(e);
                    if (ina < 0)
                    {
                        //снимаем все выделения
                        molecule.resetCheck();
                        textBox.Visible = false;
                        paint();
                    }
                    else
                    {
                        //Можно сделать так: если есть выделенные атомы, то снять выделения
                        //Если нет, то вызываем контекстное меню
                        //Вызываем контекстное меню
                        Point pt = ant.PointToScreen(e.Location);
                        molecule.RightChecked = ina;
                        contextMenuStrip1.Show(pt);
                    }
                }
                if (e.Button == MouseButtons.Left)
                {
                    atomCheckedMove = inAtom(e);
                    if (atomCheckedMove >= 0)
                    {
                        current.copy(molecule.getAtom(atomCheckedMove).Coordinates);
                        paint();
                    }
                    else
                    {
                        //включаем режим ротейт
                    }
                }
            }
            else // Если выбран режим редактирования молекулы
            {
                if (e.Button == MouseButtons.Right)
                {
                    //Если мышка попала на атом, то удаляем его
                    atomCheckedDelete = inAtom(e);
                    if (atomCheckedDelete >= 0) paint();
                }
                if (e.Button == MouseButtons.Left)
                {
                    //Добавляем на экран атом в то месте, где находится мышка
                    float w, h;
                    if ((float)ant.Width <= (float)ant.Height)
                    {
                        w = viewSize;
                        h = viewSize * (float)ant.Height / (float)ant.Width;
                    }
                    else
                    {
                        w = viewSize * (float)ant.Width / (float)ant.Height;
                        h = viewSize;
                    }
                    Vector mouse = new Vector(e.Location.X - ant.Location.X,
                        e.Location.Y - ant.Location.Y + 29, 0);
                    mouse.X = (mouse.X - ant.Width / 2) * 2 * w / (ant.Width * scale);
                    mouse.Y = (ant.Height / 2 - mouse.Y) * 2 * h / (ant.Height * scale);
                    mouse = mouse * !q;
                    if (molecule.K > 0)
                    {
                        mouse.X += molecule.Center.X;
                        mouse.Y += molecule.Center.Y;
                    }
                    molecule.addAtom(nameOfNewAtom, mouse);
                    molecule.setBonds();
                    paint();
                }
            }
            
        }

        private void ant_MouseUp(object sender, MouseEventArgs e)
        {
            BmouseIsDown = false;
            if (atomCheckedMove >= 0 || atomCheckedDelete >= 0)
            {
                if (atomCheckedMove >= 0 &&
                    (current & molecule.getAtom(atomCheckedMove).Coordinates) < 0.01)
                    BMoveRegime = false;
                if (atomCheckedMove >= 0 && !BMoveRegime 
                    && !molecule.alreadyChecked(atomCheckedMove))
                {
                    switch (molecule.numberChecked)
                    {
                        case 0: molecule.First = atomCheckedMove; break;
                        case 1: molecule.Second = atomCheckedMove; break;
                        case 2: molecule.Third = atomCheckedMove; break;
                        case 3: molecule.Forth = atomCheckedMove; break;
                    }
                }
                if (atomCheckedDelete >= 0) molecule.deleteAtom(atomCheckedDelete);
                atomCheckedMove = -1;
                atomCheckedDelete = -1;
                paint();
            }
            BMoveRegime = false;
        }

        private void ant_MouseMove(object sender, MouseEventArgs e)
        {
            if (BmouseIsDown && e.Button==MouseButtons.Left && !BEditRegime 
                && e.X > 0 && e.X < ant.Width && e.Y > 0 && e.Y < ant.Height)
            {
                int dx = e.Location.X - mouseX;
                int dy = e.Location.Y - mouseY;

                if ((dx != 0 || dy != 0) && atomCheckedMove < 0 && atomCheckedDelete < 0)
                {
                    float angle = getAngle(dx, dy);
                    Vector axis = getAxis(dx, dy);
                    axis.normolize();
                    axis = axis * !q;
                    Quaternion p = new Quaternion(axis, angle);
                    q = q * p;
                    paint();
                }

                if (atomCheckedMove>=0)
                {
                    float w, h;
                    if ((float)ant.Width <= (float)ant.Height)
                    {
                        w = viewSize;
                        h = viewSize * (float)ant.Height / (float)ant.Width;
                    }
                    else
                    {
                        w = viewSize * (float)ant.Width / (float)ant.Height;
                        h = viewSize;
                    }
                    float deltaX = 2 * w * dx / ant.Width;
                    float deltaY = 2 * h * dy / ant.Height;
                    Vector disp = new Vector(molecule.Center.X,
                        molecule.Center.Y, molecule.Center.Z);
                    Vector atom = new Vector((molecule.getAtom(atomCheckedMove).X - disp.X) * scale,
                        (molecule.getAtom(atomCheckedMove).Y - disp.Y) * scale,
                        (molecule.getAtom(atomCheckedMove).Z - disp.Z) * scale);
                    atom = atom * q;
                    atom.X += deltaX;
                    atom.Y -= deltaY;
                    atom = atom * !q;
                    molecule.getAtom(atomCheckedMove).X = atom.X / scale + disp.X;
                    molecule.getAtom(atomCheckedMove).Y = atom.Y / scale + disp.Y;
                    molecule.getAtom(atomCheckedMove).Z = atom.Z / scale + disp.Z;
                    molecule.setBonds();
                    paint();
                    //if (dx + dy > 1)
                        BMoveRegime = true;
                }
            }
            mouseX = e.Location.X;
            mouseY = e.Location.Y;
        }

        // При вращении колёсика мыши меняется масштаб
        private void ant_MouseWheel(object sender, MouseEventArgs e)
        {
            float d;
            if (scale > 15 && scale < 30)
            {
                d = (float)e.Delta / 100.0f;
            }
            else if (scale > 30)
            {
                d = (float)e.Delta / 50.0f;
            }
            else
            {
                d = (float)e.Delta / 200.0f;
            }

            if ((scale + d) > 0)
            {
                scale += d;
            }
            paint();
        }

        // Эти две функции получают угол и ось вращения из смещения мышки по экрану
        // при нажатой левой кнопке
        private float getAngle(int dx, int dy)
        {
            if (dy >= 0)
                return (float)((Math.Sqrt(dx * dx + dy * dy))
                    * 3.14 / 180); // в радианах
            return (float)(-(Math.Sqrt(dx * dx + dy * dy))
                    * 3.14 / 180); // в радианах
        }

        private Vector getAxis(int dx, int dy)
        {
            if (dy >= 0)
                return new Vector(dy, dx, 0);
            return new Vector(-dy, -dx, 0);
        }

        private void creatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            molecule = new Molecule();
            BMovie = false;
            initialize();
            scale = 30f;
            tune();
            paint();
        }

        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nameOfNewAtom = "H";
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nameOfNewAtom = "C";
        }

        private void oToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nameOfNewAtom = "O";
        }

        private void nToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nameOfNewAtom = "N";
        }

        private void numbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BNumbersIsChecked) BNumbersIsChecked = false;
            else BNumbersIsChecked = true;
            paint();
        }

        private int inAtom(MouseEventArgs e)
        {
            int aChecked = -1;
            Vector mouse = new Vector(e.Location.X - ant.Location.X,
                e.Location.Y - ant.Location.Y + 29, 0);
            float w, h;
            if ((float)ant.Width <= (float)ant.Height)
            {
                w = viewSize;
                h = viewSize * (float)ant.Height / (float)ant.Width;
            }
            else
            {
                w = viewSize * (float)ant.Width / (float)ant.Height;
                h = viewSize;
            }
            mouse.X = (mouse.X - ant.Width / 2) * 2 * w / ant.Width;
            mouse.Y = (ant.Height / 2 - mouse.Y) * 2 * h / ant.Height;
            float z = -viewSize;  //Для z-буфера
            Vector atom = new Vector();
            Vector disp = new Vector(molecule.Center.X,
                molecule.Center.Y, molecule.Center.Z);
            for (int i = 0; i < molecule.K; i++)
            {
                atom.setVector((molecule.getAtom(i).X - disp.X) * scale,
                    (molecule.getAtom(i).Y - disp.Y) * scale,
                    (molecule.getAtom(i).Z - disp.Z) * scale);
                atom = atom * q;
                if (((mouse.X - atom.X) * (mouse.X - atom.X)
                    + (mouse.Y - atom.Y) * (mouse.Y - atom.Y))
                        < radius * radius * scale * scale) // если координаты мышки попадаюn в атом
                {
                    if (atom.Z > z)
                    {
                        aChecked = i;
                        z = atom.Z;
                    }
                }
            }
            return aChecked;
        }

        private void addAndDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!BMovie)
            {
                BEditRegime = true;
                //снимаем все выделения
                molecule.resetCheck();
                textBox.Visible = false;
                paint();
            }
        }

        private void rotateAndMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BEditRegime = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (BMovie)
            {
                if(frameN == frames.Length - 1)
                {
                    timer1.Enabled = false;
                }
                else
                {
                    frameN++;
                    textBox1.Text = Convert.ToString(frameN);
                    trackBarFrame.Value = frameN;
                    molecule.copy(frames[frameN]);
                    molecule.setBonds();
                    paint();
                }
            }
            else
            {
                int dx = 3, dy = 0;
                float angle = getAngle(dx, dy);
                Vector axis = getAxis(dx, dy);
                axis.normolize();
                axis = axis * !q;
                Quaternion p = new Quaternion(axis, angle);
                q = q * p;
                paint();
            }

        }

        private void playToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (!BMovie) timer1.Interval = 100;
            else timer1.Interval = 130 - (trackBarSpeed.Value - 130);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (BMovie)
            {
                frameN = 0;
                textBox1.Text = Convert.ToString(frameN);
                trackBarFrame.Value = frameN;
                molecule.copy(frames[frameN]);
                molecule.setBonds();
                paint();
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void playManager_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (!BMovie) timer1.Interval = 100;
            else timer1.Interval = 130 - (trackBarSpeed.Value - 130);
        }

        private void pauseManager_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void stopManager_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (BMovie)
            {
                frameN = 0;
                textBox1.Text = Convert.ToString(frameN);
                trackBarFrame.Value = frameN;
                molecule.copy(frames[frameN]);
                molecule.setBonds();
                paint();
            }
        }

        private void frameBackManager_Click(object sender, EventArgs e)
        {
            if (frameN > 0)
            {
                frameN--;
                textBox1.Text = Convert.ToString(frameN);
                trackBarFrame.Value = frameN;
                molecule.copy(frames[frameN]);
                molecule.setBonds();
                paint();
            }
        }

        private void frameForwardManager_Click(object sender, EventArgs e)
        {
            if (BMovie)
            {
                if(frameN < frames.Length - 1)
                {
                    frameN++;
                    textBox1.Text = Convert.ToString(frameN);
                    trackBarFrame.Value = frameN;
                    molecule.copy(frames[frameN]);
                    molecule.setBonds();
                    paint();
                }
            }
        }

        private void movieManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BMovie)
            {
                if (BMovieManager) BMovieManager = false;
                else BMovieManager = true;
                if (BMovieManager) groupBox1.Visible = true;
                else groupBox1.Visible = false;
            }
        }

        private void setManager_Click(object sender, EventArgs e)
        {
            int a;
            if (Int32.TryParse(textBox1.Text, out a))
            {
                if(a < frames.Length && a > -1)
                {
                    frameN = a;
                    trackBarFrame.Value = frameN;
                    molecule.copy(frames[frameN]);
                    molecule.setBonds();
                    paint();
                }
                else
                {
                    textBox1.Text = Convert.ToString(frameN);
                }
            }
            else
            {
                textBox1.Text = Convert.ToString(frameN);
            }
        }

        private void trackBarFrame_Scroll(object sender, EventArgs e)
        {
            frameN = trackBarFrame.Value;
            textBox1.Text = Convert.ToString(frameN);
            molecule.copy(frames[frameN]);
            molecule.setBonds();
            paint();
        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 130 - (trackBarSpeed.Value - 130);
        }

        // Релаксация с помощью ехе-файла
        private void startRelaxingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (molecule.K > 1 && !BMovie)
            {
                // Сохраняем молекулу в файл input.txt
                using (StreamWriter sw = new StreamWriter("coord.ini"))
                {
                    String[] lines = molecule.getString();
                    foreach (string line in lines)
                    {
                        sw.WriteLine(line);
                    }
                }
                // Запускаем файл relax.exe
                Process myProcess;
                Stopwatch stopw = new Stopwatch();
                stopw.Start();
                myProcess = Process.Start("relax.exe");
                myProcess.EnableRaisingEvents = true;
                myProcess.WaitForExit();
                stopw.Stop();
                long worktime = stopw.ElapsedMilliseconds;
                TimeSpan times = stopw.Elapsed;
                if (myProcess.HasExited)
                {
                    myProcess.Close();
                    MessageBox.Show(Convert.ToString(worktime) + "\n"
                        + Convert.ToString(times.Hours) + "   "
                        + Convert.ToString(times.Minutes) + "   "
                        + Convert.ToString(times.Seconds) + "   "
                        + Convert.ToString(times.Milliseconds) + "   ");
                }
                // Открываем файл longfilm.txt
                /*
                using (StreamReader sr = new StreamReader("longfilm"))
                {
                    openFile(sr);
                }
                 * */
                using (StreamReader sr = new StreamReader("coord.rel"))
                {
                    openFile(sr);
                }
            }
            else
            {
                MessageBox.Show("В молекуле меньше 2ух атомов");
            }
        }


        unsafe private void startRelaxingDllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (molecule.K > 1 && !BMovie)
            {
                if (!Data.BRelaxing)
                {
                    // Проверяем: создана ли форма? Если да, то...
                    Form3 f3 = new Form3(); // Создаём форму 
                    f3.Owner = this; // для отображения инфы о процессе релаксации
                    f3.Show();
                    relaxing = new Thread(relaxingDLL);
                    relaxing.IsBackground = true; //!!! Основной или фоновый?
                    timerRelaxing.Start();
                    relaxing.Start();
                }
                else
                {
                    MessageBox.Show("В данный момент релаксация уже запущена!");
                }
            }
            else
            {
                MessageBox.Show("В молекуле меньше двух атомов!");
            }
        }

        unsafe private void relaxingDLL()
        {
            double[] mx = new double[molecule.K];
            double[] my = new double[molecule.K];
            double[] mz = new double[molecule.K];
            int[] mt = new int[molecule.K];
            for (int i = 0; i < molecule.K; i++)
            {
                mx[i] = molecule.getAtom(i).X;
                my[i] = molecule.getAtom(i).Y;
                mz[i] = molecule.getAtom(i).Z;
                if (molecule.getAtom(i).N == 1) mt[i] = 1;
                else if (molecule.getAtom(i).N == 6) mt[i] = 2;
                else if (molecule.getAtom(i).N == 7) mt[i] = 3;
                else if (molecule.getAtom(i).N == 8) mt[i] = 4;
            }
            int nmas = molecule.K;
            Data.BRelaxing = true;
            smain(mx, my, mz, mt, ref nmas, callBack);
            for (int i = 0; i < molecule.K; i++)
            {
                molecule.getAtom(i).X = (float)mx[i];
                molecule.getAtom(i).Y = (float)my[i];
                molecule.getAtom(i).Z = (float)mz[i];
            }
        }

        /*
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] mx = new double[molecule.K];
            double[] my = new double[molecule.K];
            double[] mz = new double[molecule.K];
            int[] mt = new int[molecule.K];
            for (int i = 0; i < molecule.K; i++)
            {
                mx[i] = molecule.getAtom(i).X;
                my[i] = molecule.getAtom(i).Y;
                mz[i] = molecule.getAtom(i).Z;
                if (molecule.getAtom(i).N == 1) mt[i] = 1;
                else if (molecule.getAtom(i).N == 6) mt[i] = 2;
                else if (molecule.getAtom(i).N == 7) mt[i] = 3;
                else if (molecule.getAtom(i).N == 8) mt[i] = 4;
            }
            int nmas = molecule.K;
            if (molecule.K > 1 && !BMovie)
            {
                //smain(mx, my, mz, mt, ref nmas);
                MessageBox.Show("Релаксация закончилась!");
                for (int i = 0; i < molecule.K; i++)
                {
                    molecule.getAtom(i).X = (float)mx[i];
                    molecule.getAtom(i).Y = (float)my[i];
                    molecule.getAtom(i).Z = (float)mz[i];
                }
                paint();
            }
            else
            {
                MessageBox.Show("В молекуле меньше двух атомов!");
            }
        }
         * */

        private void setColorOfAtomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Предлагаем пользователю задать цвет атома
            ColorDialog MyDialog = new ColorDialog();
            // Sets the initial color select to the current text color.
            //MyDialog.Color = molecule.getAtom(molecule.rightChecked);
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Vector col = new Vector();
                col.X = (float)MyDialog.Color.R / 256;
                col.Y = (float)MyDialog.Color.G / 256;
                col.Z = (float)MyDialog.Color.B / 256;
                molecule.getAtom(molecule.RightChecked).Color = col;
                molecule.RightChecked = -1;
                paint();
            }
        }

        private void setColorOfAtomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (molecule.K > 0)
            {
                Data.N = molecule.K;
                Form2 f = new Form2();
                f.Owner = this;
                f.ShowDialog();
                // Выполняем операции с данными
                if (Data.Done == true)
                {
                    for (int i = Data.Nstart; i <= Data.Nfinish; i++)
                    {
                        molecule.getAtom(i).Color.X = (float)Data.Ncolor.R / 256;
                        molecule.getAtom(i).Color.Y = (float)Data.Ncolor.G / 256;
                        molecule.getAtom(i).Color.Z = (float)Data.Ncolor.B / 256;
                        molecule.getAtom(i).Transparency = Data.Nalpha;
                    }
                    paint();
                }
                Data.Done = false;
            }
            else
            {
                MessageBox.Show("В молекуле нет атомов!");
            }
        }

        private void timerRelaxing_Tick(object sender, EventArgs e)
        {
            if (!relaxing.IsAlive)
            {
                Data.BRelaxing = false;
                timerRelaxing.Stop();
                paint();
            }
            else
            {
                molecule.setBonds();
                paint();
                // Если поток жив
                // то перерисовываем молекулу
                // перед этим в фортрановской программе изменяем массивы
                // И не надо никаких callback'ов
                // !!! Надо проверить, что это работает
            }
            if (Data.BStopThread)
            {
                relaxing.Abort();
                Data.BStopThread = false;
                //relaxing.Join();
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            paint();
        }

        unsafe public void callBack(double* mx, double* my, double* mz, ref int nm)
        {
            for (int i = 0; i < molecule.K; i++)
            {
                molecule.getAtom(i).X = (float)mx[i];
                molecule.getAtom(i).Y = (float)my[i];
                molecule.getAtom(i).Z = (float)mz[i];
            }
        }

        private void setSizesOfAtomsAndBondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Тут вызываем вспомогательную форму
            Data.RadiusA = radius;
            Data.RadiusB = radiusBond;
            //Form4 f = new Form4();
            //f.Owner = this;
            //f.ShowDialog();
            if (Data.Done == true)
            {
                radius = Data.RadiusA;
                radiusBond = Data.RadiusB;
            }
        }

        private void hToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nameOfNewAtom = "H";
        }

        private void cToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nameOfNewAtom = "C";
        }

        private void nToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nameOfNewAtom = "N";
        }

        private void oToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nameOfNewAtom = "O";
        }
    }
}
