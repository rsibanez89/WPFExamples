using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class ItemRetornablePlanillaDeCarga : INotifyPropertyChanged
    {
        private int id;
        private int idReparto;
        private ProductoRetornable retornable;
        private int sale;
        private int entra;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IdReparto
        {
            get { return idReparto; }
        }

        public int IdRetornable
        {
            get { return retornable.ID; }
        }

        public string Detalle
        {
            get { return retornable.Tipo; }
        }

        public int Sale
        {
            get { return sale; }
        }

        public int Entra
        {
            get { return entra; }
            set { entra = value; OnPropertyChanged("Entra"); OnPropertyChanged("Venta");  }
        }

        public int Venta
        {
            get { return sale - entra; }
        }

        public ItemRetornablePlanillaDeCarga(int id, int idReparto, ProductoRetornable retornable, int sale)
        {
            this.id = id;
            this.idReparto = idReparto;
            this.retornable = retornable;
            this.sale = sale;
            this.entra = 0;
        }

        public void addSale(int cantidad)
        {
            this.sale += cantidad;
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
