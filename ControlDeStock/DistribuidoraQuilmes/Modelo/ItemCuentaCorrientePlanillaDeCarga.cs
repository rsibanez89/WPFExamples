using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class ItemCuentaCorrientePlanillaDeCarga : INotifyPropertyChanged
    {
        private Cliente cliente;
        private ItemCuentaCorriente item;

        public event PropertyChangedEventHandler PropertyChanged;

        public string RazonSocial
        {
            get { return cliente.RazonSocial; }
        }

        public string Tipo
        {
            get { return item.TipoCC; }
        }

        public string Detalle
        {
            get { return item.Detalle; }
        }

        public float Monto
        {
            get { return item.Monto; }
        }

        public ItemCuentaCorrientePlanillaDeCarga(Cliente cliente, ItemCuentaCorriente item)
        {
            this.cliente = cliente;
            this.item = item;
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
