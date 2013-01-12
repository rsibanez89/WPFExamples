using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class Producto : INotifyPropertyChanged
    {
        private int id;
        private int codigo;
        private string detalle;
        private float precio;
        private int stock;
        private DateTime fechaModificado;
        private bool eliminado;
        private ProductoRetornable retornable;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; FechaModificado = DateTime.Now.ToString("dd/MM/yyyy"); OnPropertyChanged("Codigo"); }
        }

        public string Detalle
        {
            get { return detalle; }
            set { detalle = value; FechaModificado = DateTime.Now.ToString("dd/MM/yyyy"); OnPropertyChanged("Detalle"); }
        }

        public float Precio
        {
            get { return precio; }
            set { precio = value; FechaModificado = DateTime.Now.ToString("dd/MM/yyyy"); OnPropertyChanged("Precio"); }
        }

        public int Stock
        {
            get { return stock; }
            set { stock = value; FechaModificado = DateTime.Now.ToString("dd/MM/yyyy"); OnPropertyChanged("Stock"); }
        }

        public string FechaModificado
        {
            get { return fechaModificado.ToString("dd/MM/yyyy"); }
            set { this.fechaModificado = DateTime.Parse(value); OnPropertyChanged("FechaModificado"); }
        }

        public bool Eliminado
        {
            get { return eliminado; }
            set { this.eliminado = value; FechaModificado = DateTime.Now.ToString("dd/MM/yyyy"); OnPropertyChanged("Eliminado"); }
        }

        public int IdRetornable
        {
            get { return retornable.ID; }
        }

        public ProductoRetornable Retornable
        {
            get { return retornable; }
            set { retornable = value; OnPropertyChanged("IdRetornable"); }
        }

        public Producto(int id, int codigo, string detalle, int idRetornable, float precio, int stock, DateTime fechaModificado)
        {
            this.id = id;
            this.codigo = codigo;
            this.detalle = detalle;
            this.retornable = DistribuidoraQuilmes.Modelo.Retornables.getInstance()[idRetornable - 1];
            this.precio = precio;
            this.stock = stock;
            this.fechaModificado = fechaModificado;
        }

        public bool equals(Producto p)
        {
            if ((p.ID == ID) && (p.Codigo == Codigo) && (p.Detalle == Detalle) && (p.Precio == Precio)
               && (p.Stock == Stock))
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
