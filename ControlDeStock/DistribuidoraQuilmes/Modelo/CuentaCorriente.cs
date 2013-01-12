using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using DistribuidoraQuilmes.ConexionBase;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class CuentaCorriente : ObservableCollection<ItemCuentaCorriente>
    {
        public static readonly string nombreTabla = "CuentaCorriente";
        private int idVenta;

        public int IdVenta
        {
            get { return idVenta; }
        }

        public CuentaCorriente(int idVenta)
        {
            this.idVenta = idVenta;

            DataSet dataSet = MiddleDBAccess.getDataset(nombreTabla, "idventa", idVenta.ToString());

            // Carga los items de la cuenta corriente de la venta
            foreach (DataRow pRow in dataSet.Tables[nombreTabla].Rows)
            {
                int iditem = System.Convert.ToInt32(pRow["id"].ToString());
                int idtipocc = System.Convert.ToInt32(pRow["idtipocc"].ToString());
                string detalle = pRow["detalle"].ToString();
                float monto = System.Convert.ToSingle(pRow["monto"].ToString());

                ItemCuentaCorriente item = new ItemCuentaCorriente(iditem, idVenta, idtipocc, detalle, monto);
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
                this.Add(item);
            }  
        }

        private void propiedadCambiada(object sender, PropertyChangedEventArgs info)
        {
            ItemCuentaCorriente item = (ItemCuentaCorriente)sender;
            object value = sender.GetType().GetProperty(info.PropertyName).GetValue(sender, null);
            MiddleDBAccess.update(nombreTabla, item.ID, info.PropertyName, value);
        }

        public void addNewItemCuentaCorriente()
        {
            ItemCuentaCorriente item = new ItemCuentaCorriente(-1, IdVenta, 1, "Detalle", 0);
            item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
            MiddleDBAccess.addNewItemCuentaCorriente(item);
            this.Add(item);
        }

        public void deleteItemCuentaCorriente(ItemCuentaCorriente item)
        {
            MiddleDBAccess.remove(nombreTabla, item.ID);
            this.Remove(item);
        }
    }
}
