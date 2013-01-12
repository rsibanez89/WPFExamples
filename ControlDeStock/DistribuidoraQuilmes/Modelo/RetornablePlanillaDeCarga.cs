using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using System.ComponentModel;
using DistribuidoraQuilmes.ConexionBase;

namespace DistribuidoraQuilmes.Modelo
{
    public class RetornablePlanillaDeCarga : ObservableCollection<ItemRetornablePlanillaDeCarga>
    {
        private Reparto reparto;
        public static readonly string nombreTablaEntra = "EntraRetornable";

        public Reparto Repart
        {
            get { return reparto; }
        }

        public RetornablePlanillaDeCarga(Reparto reparto)
        {
            this.reparto = reparto;
        }

        public void actualizarPlanilla()
        {
            this.Clear();

            for (int v = 0; v < reparto.Count; v++)
            {
                Venta venta = reparto[v];
                for (int i = 0; i < venta.Count; i++)
                {
                    ItemVenta itemVenta = venta[i];
                    if ((itemVenta.Cantidad != 0) && (itemVenta.Producto.Retornable.ID != 0))
                    {
                        ItemRetornablePlanillaDeCarga item = itemAt(itemVenta.Producto.IdRetornable);
                        if (item == null)
                        {
                            item = new ItemRetornablePlanillaDeCarga(-1, reparto.ID, itemVenta.Producto.Retornable, itemVenta.Cantidad);
                            item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
                            this.Add(item);
                        }
                        else
                            item.addSale(itemVenta.Cantidad);
                    }
                }
            }

            DataSet dataSet = MiddleDBAccess.getDataset(nombreTablaEntra, "idreparto", reparto.ID.ToString());

            // Carga los items de lo que entra
            foreach (DataRow pRow in dataSet.Tables[nombreTablaEntra].Rows)
            {
                int iditem = System.Convert.ToInt32(pRow["id"].ToString());
                int idreparto = System.Convert.ToInt32(pRow["idreparto"].ToString());
                int idretornable = System.Convert.ToInt32(pRow["idretornable"].ToString());
                int entra = System.Convert.ToInt32(pRow["cantidad"].ToString());

                ItemRetornablePlanillaDeCarga item = itemAt(idretornable);
                if (item != null)
                {
                    item.ID = iditem;
                    item.Entra = entra;
                }
            }
        }

        private void propiedadCambiada(object sender, PropertyChangedEventArgs info)
        {
            ItemRetornablePlanillaDeCarga item = (ItemRetornablePlanillaDeCarga)sender;
            if (item.ID == -1)
            {
                if (item.Entra != 0)
                    MiddleDBAccess.addNewItemRetornablePlanillaDeCarga(item);
            }
            else
            {
                if (item.Entra != 0)
                    MiddleDBAccess.update(nombreTablaEntra, item.ID, "cantidad", item.Entra);
                else
                    MiddleDBAccess.remove(nombreTablaEntra, item.ID);
            }
        }

        private ItemRetornablePlanillaDeCarga itemAt(int idRetornable)
        {
            for (int i = 0; i < this.Count; i++)
                if (this[i].IdRetornable == idRetornable)
                    return this[i];
            return null;
        }
    }
}
