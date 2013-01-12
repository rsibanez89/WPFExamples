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
    public class Repartos : ObservableCollection<Reparto>
    {

        public static readonly string nombreTabla = "Repartos";
        private static Repartos instance;

        public static Repartos getInstance()
        {
            if (instance == null)
                instance = new Repartos();
            return instance;
        }

        private Repartos() : base()
        {
            DataSet dataSet = MiddleDBAccess.getDataset(nombreTabla);

            foreach (DataRow pRow in dataSet.Tables[nombreTabla].Rows)
            {
                int id = System.Convert.ToInt32(pRow["id"]);
                int idciudad = System.Convert.ToInt32(pRow["idciudad"]);
                DateTime fecha = (DateTime)pRow["fecha"];
                Reparto r = new Reparto(id, idciudad, fecha);
                this.Add(r);
            }
        }

        public Reparto addNewReparto(int idCiudad, DateTime fecha)
        {
            Reparto r = MiddleDBAccess.addNewReparto(idCiudad, fecha);
            this.Add(r);
            return r;
        }

        public void deleteReparto(Reparto r)
        {
            MiddleDBAccess.remove(nombreTabla, r.ID);
            this.Remove(r);
        }
    }
}
