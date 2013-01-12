using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using DistribuidoraQuilmes.ConexionBase;

namespace DistribuidoraQuilmes.Modelo
{
    public class Productos : ObservableCollection<Producto>
    {
        public static readonly string nombreTabla = "Productos";
        private static Productos instance;
        private static List<Producto> eliminados;

        public static Productos getInstance()
        {
            if(instance == null)
                instance = new Productos();
            return instance;
        }

        private Productos() : base()
        {
            eliminados = new List<Producto>();
            DataSet dataSet = MiddleDBAccess.getDataset(nombreTabla);

            foreach (DataRow pRow in dataSet.Tables[nombreTabla].Rows)
            {
                int id = System.Convert.ToInt32(pRow["id"]);
                int codigo = System.Convert.ToInt32(pRow["codigo"]);
                string detalle = pRow["detalle"].ToString();
                int idRetornable = System.Convert.ToInt32(pRow["idretornable"]);
                float precio = System.Convert.ToSingle(pRow["precio"]);
                int stock = System.Convert.ToInt32(pRow["stock"]);
                DateTime fechaModificado = (DateTime)pRow["fechamodificado"];
                bool eliminado = System.Convert.ToBoolean(pRow["eliminado"]);
                Producto p = new Producto(id, codigo, detalle, idRetornable, precio, stock, fechaModificado);
                p.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
                if(!eliminado)
                    this.Add(p);
                else
                    eliminados.Add(p);

            }
        }

        public void addNewProducto()
        {
            Producto p = MiddleDBAccess.addNewProducto();
            p.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
            this.Add(p);
        }

        public void deleteProducto(Producto p)
        {
            eliminados.Add(p);
            p.Eliminado = true;
            this.Remove(p);
        }

        private void propiedadCambiada(object sender, PropertyChangedEventArgs info)
        {
            Producto p = (Producto)sender;
            object value = sender.GetType().GetProperty(info.PropertyName).GetValue(sender, null);
            MiddleDBAccess.update(nombreTabla, p.ID, info.PropertyName, value);
        }

        public Producto productoAt(int idProducto)
        {
            for (int i = 0; i < this.Count; i++)
                if (this[i].ID == idProducto)
                    return this[i];
            for (int i = 0; i < eliminados.Count; i++)
                if (eliminados[i].ID == idProducto)
                    return eliminados[i];
            return null;
        }
    }
}
