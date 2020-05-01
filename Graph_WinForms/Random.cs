using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph_WinForms
{
    public partial class Form2 : Form
    {
        static readonly Random rnd = new Random();
        public Form2()
        {
            InitializeComponent();
        }

        private void VNRandom_Click(object sender, EventArgs e)
        {
            NumOfVertices.Value = rnd.Next(3, 21);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MainWindow.chosenNumber = (int)NumOfVertices.Value;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            MainWindow.chosenNumber = -1;
            this.Close();
        }
    }
}
