using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using DistribuidoraQuilmes.ConexionBase;

namespace DistribuidoraQuilmes.Modelo
{
    public class Retornables: ObservableCollection<ProductoRetornable>
    {
        private static Retornables instance;
        public static readonly string nombreTablaRetornable = "Retornable";

        private Retornables()
        {
            DataSet dataSet = MiddleDBAccess.getDataset(nombreTablaRetornable);

            foreach (DataRow pRow in dataSet.Tables[nombreTablaRetornable].Rows)
            {
                int id = System.Convert.ToInt32(pRow["id"]);
                string tipo = pRow["tipo"].ToString();
                float precio = System.Convert.ToSingle(pRow["precio"]);

                ProductoRetornable item = new ProductoRetornable(id, tipo, precio);
                this.Add(item);
            }
        }

        public static Retornables getInstance()
        {
            if (instance == null)
                instance = new Retornables();
            return instance;
        }

    }
}
