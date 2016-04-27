using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucero
{
    class Cliente
    {
        private string nombre;
        private string id;
        private int personas;

        public Cliente(string nombre, string id, int personas)
        {
            this.nombre = nombre;
            this.id = id;
            this.personas = personas;
        }

        /*
        public void muestra()
        {
            Console.WriteLine(nombre + " " + id + " " + personas);
        }
        */

        public string getNombre()
        {
            return this.nombre;
        }

        public string getId()
        {
            return this.id;
        }

        public int getPersonas()
        {
            return this.personas;
        }

        public void agregarPersonas(int num)
        {
            this.personas += num;
        }
    }
}
