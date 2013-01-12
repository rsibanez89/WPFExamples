using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class ProductoRetornable: INotifyPropertyChanged
    {
        private int id;
        private string tipo;
        private float precio;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; OnPropertyChanged("Tipo"); }
        }

        public float Precio
        {
            get { return precio; }
            set { precio = value; OnPropertyChanged("Precio"); }
        }

        public ProductoRetornable(int id, string tipo, float precio)
        {
            this.id = id;
            this.tipo = tipo;
            this.precio = precio;
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
