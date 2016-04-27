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
                while(line != null && line != "")
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
                                string directory = Directory.GetCurrentDirectory();
                                directory += "\\Archivos-cruceros\\Crucero_" + numCruceros + "_" + crucero.getPuerto() + "_" + crucero.getDestino() + ".txt";
                                StreamWriter outputFileClientes;
                                if (crucero.getClientes() == 0)
                                {
                                    outputFileClientes = File.CreateText(directory);
                                    outputFileClientes.Close();
                                }
                                else
                                {
                                    crucero.importarClientes(directory);
                                }
                                break;
                        }
                    }
                    crucero.setId(numCruceros);
                    listaCruceros.Add(crucero);
                    lstbxLista.Items.Add(crucero.getId() + ". Fecha de salida: " + crucero.getFechaSalida() + ". Destino: " + crucero.getDestino() + ". $" + string.Format("{0:0.00}", crucero.getCosto()) );

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

        private void Usuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            for(int i=0; i<Application.OpenForms.Count; i++)
            {
                Application.OpenForms[i].Close();
            }
        }

        private void btnCrucerosDisponibles_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            lblCosto.Text = "$ 0.00";
            if(!chkboxFecha.Checked)
            {
                string itemSeleccionado = cmbDestinos.SelectedItem.ToString();
                if(itemSeleccionado != "-- Cualquier destino --")
                {
                    lstbxLista.Items.Clear();
                    for(int i=0; i<numCruceros; i++)
                    {
                        if (listaCruceros[i].getDestino() == itemSeleccionado)
                            lstbxLista.Items.Add(listaCruceros[i].getId() + ". Fecha de salida: " + listaCruceros[i].getFechaSalida() + ". Destino: " + listaCruceros[i].getDestino() + ". $" + string.Format("{0:0.00}", listaCruceros[i].getCosto()) );
                    }
                }
                else
                {
                    lstbxLista.Items.Clear();
                    for (int i = 0; i < numCruceros; i++)
                    {
                        lstbxLista.Items.Add(listaCruceros[i].getId() + ". Fecha de salida: " + listaCruceros[i].getFechaSalida() + ". Destino: " + listaCruceros[i].getDestino() + ". $" + string.Format("{0:0.00}", listaCruceros[i].getCosto()) );
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
                            lstbxLista.Items.Add(listaCruceros[i].getId() + ". Fecha de salida: " + listaCruceros[i].getFechaSalida() + ". Destino: " + listaCruceros[i].getDestino() + ". $" + string.Format("{0:0.00}", listaCruceros[i].getCosto()) );
                    }
                }
                else
                {
                    lstbxLista.Items.Clear();
                    for (int i = 0; i < numCruceros; i++)
                    {
                        if (listaCruceros[i].getFechaSalida() == fechaSeleccionada)
                            lstbxLista.Items.Add(listaCruceros[i].getId() + ". Fecha de salida: " + listaCruceros[i].getFechaSalida() + ". Destino: " + listaCruceros[i].getDestino() + ". $" + string.Format("{0:0.00}", listaCruceros[i].getCosto()) );
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
            try
            {
                string line = txtbxNumPasajeros.Text;
                if (lstbxLista.SelectedIndex != -1)
                {
                    if (line == "")
                        lblCosto.Text = "$ 0.00";
                    else
                    {
                        int pasajeros = int.Parse(line);
                        string selectedItem = lstbxLista.SelectedItem.ToString();
                        double costoIndividual = double.Parse(selectedItem.Substring(selectedItem.IndexOf("$") + 1));
                        lblCosto.Text = "$ " + string.Format("{0:0.00}", pasajeros * costoIndividual);
                    }
                }
                else
                {
                    lblCosto.Text = "$ 0.00";
                }
            }
            
            catch(FormatException)
            {
                MessageBox.Show("Por favor, introduzca sólo caracteres numéricos.", "Error en entrada de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtbxNumPasajeros.Clear();
            }
        }

        private void lstbxLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string itemSeleccionado = lstbxLista.SelectedItem.ToString();
                txtbxReservarCruc.Text = (itemSeleccionado == null) ? "" : itemSeleccionado;

                if (txtbxNumPasajeros.Text != "")
                {
                    string selectedItem = lstbxLista.SelectedItem.ToString();
                    double costo = double.Parse(selectedItem.Substring(selectedItem.IndexOf("$") + 1));
                    costo *= double.Parse(txtbxNumPasajeros.Text);
                    lblCosto.Text = "$ " + string.Format("{0:0.00}", costo);
                }
            }

            catch(FormatException)
            {
                txtbxNumPasajeros.Clear();
            }
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            bool canProceed = true;

            if(string.IsNullOrWhiteSpace(txtbxReservarCruc.Text))
            {
                lblReservarCrucero.ForeColor = System.Drawing.Color.Red;
                canProceed = false;
            }
            else
                lblReservarCrucero.ForeColor = System.Drawing.Color.Black;


            if (string.IsNullOrWhiteSpace(txtbxNumPasajeros.Text))
            {
                lblNumPasajeros.ForeColor = System.Drawing.Color.Red;
                canProceed = false;
            }
            else
                lblNumPasajeros.ForeColor = System.Drawing.Color.Black;


            if (string.IsNullOrWhiteSpace(txtbxNombre.Text))
            {
                lblNombre.ForeColor = System.Drawing.Color.Red;
                canProceed = false;
            }
            else
                lblNombre.ForeColor = System.Drawing.Color.Black;


            if (string.IsNullOrWhiteSpace(txtbxNumTarjeta.Text))
            {
                lblNumTarjeta.ForeColor = System.Drawing.Color.Red;
                canProceed = false;
            }
            else
                lblNumTarjeta.ForeColor = System.Drawing.Color.Black;

            if (!canProceed)
            {
                MessageBox.Show("Por favor, complete las secciones faltantes.", "Registro incompleto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                int numPasajeros = int.Parse(txtbxNumPasajeros.Text);
                string crucero = txtbxReservarCruc.Text;
                int indiceTemporal = crucero.IndexOf('.');
                int idCrucero = int.Parse(crucero.Substring(0, indiceTemporal));

                if (numPasajeros + listaCruceros[idCrucero].getPersonas() > listaCruceros[idCrucero].getMaxPersonas())
                {
                    int cupoDisponible = listaCruceros[idCrucero].getMaxPersonas() - listaCruceros[idCrucero].getPersonas();
                    if (cupoDisponible == 0)
                        MessageBox.Show("Lo sentimos, pero este crucero ya está lleno.", "Cupo lleno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Es imposible realizar la reservación. Sólo hay espacio para " + cupoDisponible + " personas más.", "Espacio insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    DialogResult continuar = MessageBox.Show("¿Está seguro de la reservación?", "Mensaje de confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if(continuar == DialogResult.Yes)
                    {
                        string nombreCliente = txtbxNombre.Text;
                        string idCliente = txtbxNumTarjeta.Text;
                        Cliente cl = new Cliente(nombreCliente, idCliente, numPasajeros);

                        listaCruceros[idCrucero].agregarPersonas(numPasajeros);
                        listaCruceros[idCrucero].agregarCliente(cl);

                        updateCrucerosFile();

                        string directory = Directory.GetCurrentDirectory();
                        directory += "\\Archivos-cruceros\\Crucero_" + idCrucero + "_" + listaCruceros[idCrucero].getPuerto() + "_" + listaCruceros[idCrucero].getDestino() + ".txt";
                        listaCruceros[idCrucero].exportarClientes(directory);

                        MessageBox.Show("Su reservación ha sido exitosa.", "Operación exitosa", MessageBoxButtons.OK ,MessageBoxIcon.Information);
                        lstbxConfirmacion.Items.Add("Huésped principal: " + nombreCliente + ". Número de pasajeros: " + numPasajeros + ". Tarjeta: " + idCliente);
                    }

                }
            }
        }


        private void updateCrucerosFile()
        {
            StreamWriter outputFile = File.CreateText(@"InfoCruceros.txt");
            for(int i=0; i<numCruceros; i++)
            {
                outputFile.Write(string.Format("{0:0.00}", listaCruceros[i].getCosto()) + "/");
                outputFile.Write(listaCruceros[i].getFechaSalida() + "/");
                outputFile.Write(listaCruceros[i].getFechaRegreso() + "/");
                outputFile.Write(listaCruceros[i].getPuerto() + "/");
                outputFile.Write(listaCruceros[i].getDestino() + "/");
                outputFile.Write(listaCruceros[i].getMaxPersonas() + "/");
                outputFile.Write(listaCruceros[i].getPersonas() + "/");
                outputFile.Write(listaCruceros[i].getClientes() + "/");
                outputFile.WriteLine();
            }
            outputFile.Close();
        }
    }
}
