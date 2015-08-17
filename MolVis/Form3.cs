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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            timerForm3.Start();
        }

        private void timerForm3_Tick(object sender, EventArgs e)
        {
            if (!Data.BRelaxing)
            {
                label1.Text = "Relaxation ended!";
                timerForm3.Stop();
                button1.Enabled = true;
                button2.Enabled = false;
            }
            else
            {
                label1.Text += ".";
                if (label1.Text == "Relaxing....")
                    label1.Text = "Relaxing";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Кнопочка ОК
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Кнопочка ОТМЕНА
            Data.BStopThread = true;
            Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Data.BRelaxing)
                Data.BStopThread = true;
        }
    }
}
