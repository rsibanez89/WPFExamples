using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using DistribuidoraQuilmes.ConexionBase;

namespace DistribuidoraQuilmes.Modelo
{
    public class Venta : ObservableCollection<ItemVenta>
    {
        public static readonly string nombreTablaItemVenta = "ItemVenta";
        public static readonly string nombreTablaProductos = "Productos";

        private int id;
        private Cliente cliente;
        private Reparto reparto;
        private CuentaCorriente cuentaCorriente;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IdCliente
        {
            get { return cliente.ID; }
        }

        public int IdReparto
        {
            get { return reparto.ID; }
        }

        public float Total
        {
            get 
            {
                float  total = 0;
                for (int i = 0; i < this.Count; i++)
                    total += this[i].Subtotal;
                return total;
            }
        }

        public CuentaCorriente CuentaCorriente
        {
            get { return cuentaCorriente; }
        }

        public Cliente Cliente
        {
            get { return cliente; }
        }

        public Reparto Reparto
        {
            get { return reparto; }
        }

        public Venta(int id, Cliente cliente, Reparto reparto)
        {
            this.id = id;
            this.cliente = cliente;
            this.reparto = reparto;
            this.cuentaCorriente = new CuentaCorriente(id);

            DataSet dataSet = MiddleDBAccess.getDataset(nombreTablaProductos);

            // Carga los todos los productos que figuran en la BD
            foreach (DataRow pRow in dataSet.Tables[nombreTablaProductos].Rows)
            {
                int idproducto = System.Convert.ToInt32(pRow["id"]);
                int codigo = System.Convert.ToInt32(pRow["codigo"]);
                string detalle = pRow["detalle"].ToString();
                float precio = System.Convert.ToSingle(pRow["precio"]);
                bool eliminado = System.Convert.ToBoolean(pRow["eliminado"]);
                if (!eliminado)
                {
                    ItemVenta item = new ItemVenta(-1, id, idproducto, 0, precio);
                    item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
                    this.Add(item);
                }
            }


            dataSet = MiddleDBAccess.getDataset(nombreTablaItemVenta, "idventa", id.ToString());

            // Carga los items reales de la venta
            foreach (DataRow pRow in dataSet.Tables[nombreTablaItemVenta].Rows)
            {
                int iditem = System.Convert.ToInt32(pRow["id"].ToString());
                int idproducto = System.Convert.ToInt32(pRow["idproducto"].ToString());
                int cantidad = System.Convert.ToInt32(pRow["cantidad"].ToString());
                float precio = System.Convert.ToSingle(pRow["precio"].ToString());

                ItemVenta item = itemAt(idproducto);
                if (item == null)
                {
                    item = new ItemVenta(iditem, id, idproducto, cantidad, precio);
                    item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
                    this.Add(item);
                }
                else
                {
                    item.ID = iditem;
                    item.Cantidad = cantidad;
                    item.Precio = precio;
                }
            }  
        }

        private void propiedadCambiada(object sender, PropertyChangedEventArgs info)
        {
            ItemVenta item = (ItemVenta)sender;
            if (item.ID == -1)
            {
                if (item.Cantidad != 0)
                    MiddleDBAccess.addNewItem(item);
            }
            else
            {
                if (item.Cantidad != 0)
                    MiddleDBAccess.update(nombreTablaItemVenta, item.ID, "cantidad", item.Cantidad);
                else
                    MiddleDBAccess.remove(nombreTablaItemVenta, item.ID);
            }
            this.OnPropertyChanged(new PropertyChangedEventArgs("Total"));
        }

        private ItemVenta itemAt(int idProducto)
        {
            for (int i = 0; i < this.Count; i++)
                if (this[i].IdProducto == idProducto)
                    return this[i];
            return null;
        }

    }
}
