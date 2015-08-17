using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MolVis
{
    public partial class Form2 : Form
    {
        int trans; // прозрачность текущая
        public Form2()
        {
            InitializeComponent();
            trans = 255;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Кнопка Choose color
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.Color = label1.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                label1.BackColor = MyDialog.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Кнопка ОК
            Data.Ncolor = label1.BackColor;
            if (textBox1.Text != "")
            {
                Data.Nstart = Convert.ToInt32(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Введите число от 0 до "+ Convert.ToString(Data.N)
                    + " в поле \"From\"!");
                return;
            }
            if (textBox2.Text != "")
            {
                Data.Nfinish = Convert.ToInt32(textBox2.Text);
            }
            else
            {
                MessageBox.Show("Введите число от 0 до " + Convert.ToString(Data.N)
                    + " в поле \"To\"!");
                return;
            }
            if (Data.Nstart > Data.Nfinish)
            {
                MessageBox.Show("Значение From должно быть меньше значения To!");
                return;
            }
            Data.Nalpha = Convert.ToInt32(textBox3.Text);
            Data.Done = true;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Кнопка Отмена
            Data.Done = false;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            int a;
            if (Int32.TryParse(textBox1.Text, out a))
            {
                if (a > Data.N || a < 0)
                {
                    textBox1.Text = "";
                    MessageBox.Show("Это должно быть число от 0 до "
                        + Convert.ToString(Data.N - 1) + "!");
                }
            }
            else
            {
                textBox1.Text = "";
                MessageBox.Show("Это должно быть число от 0 до "
                    + Convert.ToString(Data.N - 1) + "!");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "") return;
            int a;
            if (Int32.TryParse(textBox2.Text, out a))
            {
                if (a > Data.N || a < 0)
                {
                    textBox2.Text = "";
                    MessageBox.Show("Это должно быть число от 0 до "
                        + Convert.ToString(Data.N - 1) + "!");
                }
            }
            else
            {
                textBox2.Text = "";
                MessageBox.Show("Это должно быть число от 0 до "
                    + Convert.ToString(Data.N - 1) + "!");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (Int32.TryParse(textBox3.Text, out a))
            {
                if (a < 255 && a > -1)
                {
                    trans = a;
                }
                else
                {
                    textBox3.Text = Convert.ToString(trans);
                    MessageBox.Show("Это должно быть число от 0 до 255!");
                }
            }
            else
            {
                textBox3.Text = Convert.ToString(trans);
                MessageBox.Show("Это должно быть число от 0 до 255!");
            }
        }
    }
}
