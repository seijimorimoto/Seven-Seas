using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Crucero
{
    class Crucero
    {
        private double costo;
        private string fechaSalida;
        private string fechaRegreso;
        private string puerto;
        private string destino;
        private int maxPersonas;
        private int personas;
        private int clientes;
        private int id;
        private Cliente[] arrCliente;

        /*
        public void muestra()
        {
            Console.WriteLine(costo + "/" + fechaSalida + "/" + fechaRegreso + "/" + puerto + "/" + destino + "/" + maxPersonas + "/" + personas + "/" + clientes);
        }
        */

        public void setCosto(double costo)
        {
            this.costo = costo;
        }

        public double getCosto()
        {
            return this.costo;
        }

        public void setFechaSalida(string fechaSalida)
        {
            this.fechaSalida = fechaSalida;
        }

        public string getFechaSalida()
        {
            return this.fechaSalida;
        }

        public void setFechaRegreso(string fechaRegreso)
        {
            this.fechaRegreso = fechaRegreso;
        }

        public string getFechaRegreso()
        {
            return this.fechaRegreso;
        }

        public void setPuerto(string puerto)
        {
            this.puerto = puerto;
        }

        public string getPuerto()
        {
            return this.puerto;
        }

        public void setDestino(string destino)
        {
            this.destino = destino;
        }

        public string getDestino()
        {
            return this.destino;
        }

        public void setPersonas(int personas)
        {
            this.personas = personas;
        }

        public int getPersonas()
        {
            return this.personas;
        }
        
        public void setMaxPersonas(int maxPersonas)
        {
            this.maxPersonas = maxPersonas;
            arrCliente = new Cliente [maxPersonas];
        }

        public int getMaxPersonas()
        {
            return this.maxPersonas;
        }

        public void setClientes(int clientes)
        {
            this.clientes = clientes;
        }

        public int getClientes()
        {
            return this.clientes;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }

        public void agregarPersonas(int personas)
        {
            this.personas += personas;
        }

        public void importarClientes(string directory)
        {
            StreamReader inputFile = File.OpenText(directory);
            string line, nombre, numeroTarjeta;
            int pasajeros, indice;

            line = inputFile.ReadLine();
            for(int i=0; i<this.clientes; i++)
            {
                indice = line.IndexOf("/");
                nombre = line.Substring(0, indice);
                line = line.Substring(indice + 1);
                indice = line.IndexOf("/");
                numeroTarjeta = line.Substring(0, indice);
                line = line.Substring(indice + 1);
                indice = line.IndexOf("/");
                pasajeros = int.Parse(line.Substring(0, indice));

                arrCliente[i] = new Cliente(nombre, numeroTarjeta, pasajeros);

                line = inputFile.ReadLine();
            }

            inputFile.Close();

            /* Probar que se guardan todos los clientes de los archivos de texto en la matriz
            for(int i=0; i<this.clientes; i++)
            {
                arrCliente[i].muestra();
            }
            */
        }

        
        public void exportarClientes(string directory)
        {
            StreamWriter outputFile = File.CreateText(directory);
            for(int i=0; i<this.clientes; i++)
            {
                outputFile.Write(arrCliente[i].getNombre() + "/");
                outputFile.Write(arrCliente[i].getId() + "/");
                outputFile.Write(arrCliente[i].getPersonas() + "/");
                outputFile.WriteLine();
            }
            outputFile.Close();
        }


        public void agregarCliente(Cliente cl)
        {
            bool encontrado = false;
            int indice = 0;
            for(int i=0; i<this.clientes && !encontrado; i++)
            {
                if((arrCliente[i].getNombre() == cl.getNombre()) && (arrCliente[i].getId() == cl.getId()))
                {
                    encontrado = true;
                    indice = i;
                }
            }

            if(encontrado)
            {
                int num = cl.getPersonas();
                arrCliente[indice].agregarPersonas(num);
            }

            else
            {
                arrCliente[this.clientes] = cl;
                this.clientes++;
            }

            /* Probar que se actualiza el arreglo de clientes
            for(int i=0; i<this.clientes; i++)
            {
                Console.WriteLine(arrCliente[i].getNombre() + " " + arrCliente[i].getId() + " " + arrCliente[i].getPersonas());
            }
            */
        }
    }
}
