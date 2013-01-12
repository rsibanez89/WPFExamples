using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using DistribuidoraQuilmes.ConexionBase;

namespace DistribuidoraQuilmes.Modelo
{
    public class PlanillaDeCarga : ObservableCollection<ItemPlanillaDeCarga>
    {
        private Reparto reparto;
        public static readonly string nombreTablaEntra = "EntraProducto";

        public Reparto Repart
        {
            get { return reparto; }
        }

        public float Total
        {
            get
            {
                float total = 0;
                for (int i = 0; i < this.Count; i++)
                    total += this[i].Subtotal;
                return total;
            }
        }

        public PlanillaDeCarga(Reparto reparto)
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
                    if (itemVenta.Cantidad != 0)
                    {
                        ItemPlanillaDeCarga item = itemAt(itemVenta.IdProducto);
                        if (item == null)
                        {
                            item = new ItemPlanillaDeCarga(-1, reparto.ID, itemVenta.IdProducto, itemVenta.Codigo, itemVenta.Detalle, itemVenta.Cantidad, itemVenta.Precio);
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
                int idproducto = System.Convert.ToInt32(pRow["idproducto"].ToString());
                int entra = System.Convert.ToInt32(pRow["cantidad"].ToString());

                ItemPlanillaDeCarga item = itemAt(idproducto);
                if (item != null)
                {
                    item.ID = iditem;
                    item.Entra = entra;
                }
            }
        }

        private void propiedadCambiada(object sender, PropertyChangedEventArgs info)
        {
            ItemPlanillaDeCarga item = (ItemPlanillaDeCarga)sender;
            if (item.ID == -1)
            {
                if (item.Entra != 0)
                    MiddleDBAccess.addNewItemPlanillaDeCarga(item);
            }
            else
            {
                if (item.Entra != 0)
                    MiddleDBAccess.update(nombreTablaEntra, item.ID, "cantidad", item.Entra);
                else
                    MiddleDBAccess.remove(nombreTablaEntra, item.ID);
            }
            this.OnPropertyChanged(new PropertyChangedEventArgs("Total"));
        }

        private ItemPlanillaDeCarga itemAt(int idProducto)
        {
            for (int i = 0; i < this.Count; i++)
                if (this[i].IdProducto == idProducto)
                    return this[i];
            return null;
        }
    }
}
