using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class Cliente: INotifyPropertyChanged
    {
        private int id;
        private string apellido;
        private string nombre;
        private string razonSocial;
        private string telefono;
        private string movil;
        private int idCiudad;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; OnPropertyChanged("Apellido"); }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged("Nombre"); }
        }

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; OnPropertyChanged("RazonSocial"); }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value;  OnPropertyChanged("Telefono"); }
        }

        public string Movil
        {
            get { return movil; }
            set { movil = value;  OnPropertyChanged("Movil"); }
        }

        public int IdCiudad
        {
            get { return idCiudad; }
            set { idCiudad = value; OnPropertyChanged("IdCiudad"); }
        }

        public string Ciudad
        {
            get { return CiudadesList.getInstance()[idCiudad - 1]; }
            set { idCiudad = CiudadesList.index(value); OnPropertyChanged("IdCiudad"); }
        }

        public string NombreCompleto
        {
            get { return apellido + ", " + nombre; }
        }

        public Cliente(int id, string apellido, string nombre, string razonSocial, string telefono, string movil, int idCiudad)
        {
            this.id = id;
            this.apellido = apellido;
            this.nombre = nombre;
            this.razonSocial = razonSocial;
            this.telefono = telefono;
            this.movil = movil;
            this.idCiudad = idCiudad;
        }

        public bool equals(Cliente c)
        {
            if ((c.ID == ID) && (c.Apellido == Apellido) && (c.Nombre == Nombre) && (c.RazonSocial == RazonSocial) && (c.Telefono == Telefono) && (c.Movil == Movil) && (c.IdCiudad == IdCiudad))
                return true;
            return false;
        }

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
