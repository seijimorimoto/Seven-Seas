using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Crucero
{
    public partial class ProyectoFinal : Form
    {
        List<Crucero> listaCruceros = new List<Crucero>();
        List<string> listaDestinos = new List<string>();
        int numCruceros = 0;
        int numDestinos = 0;

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

            StreamReader inputFile = File.OpenText(@"InfoCruceros.txt");
            try
            {
                string line = inputFile.ReadLine();
                while(line != null)
                {
                    int indice;
                    string temp;
                    Crucero crucero = new Crucero();
                    for(int i=0; i<8; i++)
                    {
                        indice = line.IndexOf("/");
                        temp = line.Substring(0, indice);
                        line = line.Substring(indice + 1);
                        
                        switch(i)
                        {
                            case 0:
                                crucero.setCosto(double.Parse(temp));
                                break;
                            case 1:
                                crucero.setFechaSalida(temp);
                                break;
                            case 2:
                                crucero.setFechaRegreso(temp);
                                break;
                            case 3:
                                crucero.setPuerto(temp);
                                break;
                            case 4:
                                int existe = listaDestinos.IndexOf(temp);
                                if (existe == -1)
                                {
                                    listaDestinos.Add(temp);
                                    numDestinos++;
                                }
                                crucero.setDestino(temp);
                                break;
                            case 5:
                                crucero.setMaxPersonas(int.Parse(temp));
                                break;
                            case 6:
                                crucero.setPersonas(int.Parse(temp));
                                break;
                            case 7:
                                crucero.setClientes(int.Parse(temp));
                                break;
                        }
                    }
                    crucero.setId(numCruceros);
                    listaCruceros.Add(crucero);
                    lstbxLista.Items.Add(crucero.getId() + ". " + crucero.getFechaSalida() + " " + crucero.getDestino());

                    numCruceros++;
                    line = inputFile.ReadLine();

                }

                listaDestinos.Sort();
                cmbDestinos.Items.Add("-- Cualquier destino --");
                for (int i = 0; i < numDestinos; i++)
                    cmbDestinos.Items.Add(listaDestinos[i]);

                cmbDestinos.SelectedIndex = 0;

                inputFile.Close();
                
            }

            catch(FileNotFoundException e)
            {
                //AlertBox diciendo que no se encontró el archivo.
            }


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

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if(!chkboxFecha.Checked)
            {
                string itemSeleccionado = cmbDestinos.SelectedItem.ToString();
                if(itemSeleccionado != "-- Cualquier destino --")
                {
                    lstbxLista.Items.Clear();
                    for(int i=0; i<numCruceros; i++)
                    {
                        if (listaCruceros[i].getDestino() == itemSeleccionado)
                            lstbxLista.Items.Add(listaCruceros[i].getId() + ". " + listaCruceros[i].getFechaSalida() + " " + listaCruceros[i].getDestino());
                    }
                }
                else
                {
                    lstbxLista.Items.Clear();
                    for (int i = 0; i < numCruceros; i++)
                    {
                        lstbxLista.Items.Add(listaCruceros[i].getId() + ". " + listaCruceros[i].getFechaSalida() + " " + listaCruceros[i].getDestino());
                    }
                }
            }
            else
            {
                string itemSeleccionado = cmbDestinos.SelectedItem.ToString();
                string fechaSeleccionada = calCalendario.Value.ToString("dd-MM-yyyy");
                if (itemSeleccionado != "-- Cualquier destino --")
                {
                    lstbxLista.Items.Clear();
                    for (int i = 0; i < numCruceros; i++)
                    {
                        if ((listaCruceros[i].getDestino() == itemSeleccionado) && (listaCruceros[i].getFechaSalida() == fechaSeleccionada))
                            lstbxLista.Items.Add(listaCruceros[i].getId() + ". " + listaCruceros[i].getFechaSalida() + " " + listaCruceros[i].getDestino());
                    }
                }
                else
                {
                    lstbxLista.Items.Clear();
                    for (int i = 0; i < numCruceros; i++)
                    {
                        if (listaCruceros[i].getFechaSalida() == fechaSeleccionada)
                            lstbxLista.Items.Add(listaCruceros[i].getId() + ". " + listaCruceros[i].getFechaSalida() + " " + listaCruceros[i].getDestino());
                    }
                }
            }

            txtbxReservarCruc.Text = "";
        }

        private void chkboxFecha_CheckedChanged(object sender, EventArgs e)
        {
            calCalendario.Enabled = (chkboxFecha.Checked) ? true : false;
        }

        private void txtbxNumPasajeros_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstbxLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemSeleccionado = lstbxLista.SelectedItem.ToString();
            txtbxReservarCruc.Text = (itemSeleccionado == null) ? "" : itemSeleccionado;
        }
    }
}
