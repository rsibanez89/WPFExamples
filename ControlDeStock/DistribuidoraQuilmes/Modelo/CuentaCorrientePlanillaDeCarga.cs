using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class CuentaCorrientePlanillaDeCarga : ObservableCollection<ItemCuentaCorrientePlanillaDeCarga>
    {
        private Reparto reparto;

        public Reparto Repart
        {
            get { return reparto; }
        }

        public CuentaCorrientePlanillaDeCarga(Reparto reparto)
        {
            this.reparto = reparto;
        }

        public void actualizarPlanilla()
        {
            this.Clear();

            for (int v = 0; v < reparto.Count; v++)
            {
                CuentaCorriente cc = reparto[v].CuentaCorriente;
                for (int i = 0; i < cc.Count; i++)
                {
                    ItemCuentaCorrientePlanillaDeCarga item = new ItemCuentaCorrientePlanillaDeCarga(reparto[v].Cliente, cc[i]);
                    this.Add(item);
                }
            }
        }

        public float getTotal(string tipo)
        {
            float total = 0;
            for (int i = 0; i < this.Count; i++)
                if(this[i].Tipo == tipo)
                    total += this[i].Monto;
            return total;
        }
    }
}
