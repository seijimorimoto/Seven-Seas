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
    public partial class ProyectoFinal : Form
    {
        public ProyectoFinal()
        {
            InitializeComponent();
            btnCrucerosDisponibles.TabStop = false;
            btnCrucerosDisponibles.FlatStyle = FlatStyle.Flat;
            btnCrucerosDisponibles.FlatAppearance.BorderSize = 0;

            btnReservar.TabStop = false;
            btnReservar.FlatStyle = FlatStyle.Flat;
            btnReservar.FlatAppearance.BorderSize = 0;

            btnFiltrar.TabStop = false;
            btnFiltrar.FlatStyle = FlatStyle.Flat;
            btnFiltrar.FlatAppearance.BorderSize = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCrucerosDisponibles_Click(object sender, EventArgs e)
        {
            
        }

        private void btnContacto_Click(object sender, EventArgs e)
        {

        }
    }
}
