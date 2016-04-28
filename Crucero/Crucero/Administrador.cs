using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crucero
{
    public partial class Administrador : Form
    {
        public Administrador()
        {
            InitializeComponent();
        }

        private void Administrador_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Application.OpenForms[i].Close();
            }
        }

        private void Administrador_Load(object sender, EventArgs e)
        {

        }

        private void lblSevenSeas_Click(object sender, EventArgs e)
        {

        }
    }
}
