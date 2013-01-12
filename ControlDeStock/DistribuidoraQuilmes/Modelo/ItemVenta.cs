using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class ItemVenta : INotifyPropertyChanged
    {
        private int id;
        private int idVenta;
        private int idproducto;
        private int cantidad;
        private float precio;
        private Producto producto;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IdVenta
        {
            get { return idVenta; }
            set { idVenta = value; }
        }

        public int IdProducto
        {
            get { return idproducto; }
            set { idproducto = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; OnPropertyChanged("Cantidad"); OnPropertyChanged("Subtotal"); }
        }

        public float Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public float Subtotal
        {
            get { return precio * cantidad; }
        }

        public string Detalle
        {
            get { return producto.Detalle; }
        }

        public int Codigo
        {
            get { return producto.Codigo; }
        }

        public Producto Producto
        {
            get { return producto; }
        }

        public ItemVenta(int id, int idVenta, int idproducto, int cantidad, float precio)
        {
            this.id = id;
            this.idVenta = idVenta;
            this.idproducto = idproducto;
            this.cantidad = cantidad;
            this.precio = precio;
            this.producto = Productos.getInstance().productoAt(IdProducto);
        }

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public bool Equals(ItemVenta item)
        {
            if (item.IdProducto == IdProducto)
                return true;
            return false;
        }
    }
}
