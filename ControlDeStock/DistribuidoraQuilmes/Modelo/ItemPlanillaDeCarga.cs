using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class ItemPlanillaDeCarga : INotifyPropertyChanged
    {
        private int id;
        private int idReparto;
        private int idProducto;
        private int codigo;
        private string detalle;
        private int sale;
        private int entra;
        private float precio;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IdReparto
        {
            get { return idReparto; }
        }

        public int IdProducto
        {
            get { return idProducto; }
        }

        public int Codigo
        {
            get { return codigo; }
        }

        public string Detalle
        {
            get { return detalle; }
        }

        public int Sale
        {
            get { return sale; }
        }

        public int Entra
        {
            get { return entra; }
            set { entra = value; OnPropertyChanged("Entra"); OnPropertyChanged("Venta"); OnPropertyChanged("Subtotal"); }
        }

        public int Venta
        {
            get { return sale - entra; }
        }

        public float Precio
        {
            get { return precio; }
        }

        public float Subtotal
        {
            get { return Venta * Precio; }
        }

        public ItemPlanillaDeCarga(int id, int idReparto, int idProducto, int codigo, string detalle, int sale, float precio)
        {
            this.id = id;
            this.idReparto = idReparto;
            this.idProducto = idProducto;
            this.codigo = codigo;
            this.detalle = detalle;
            this.sale = sale;
            this.entra = 0;
            this.precio = precio;
        }

        public void addSale(int cantidad)
        {
            this.sale += cantidad;
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
