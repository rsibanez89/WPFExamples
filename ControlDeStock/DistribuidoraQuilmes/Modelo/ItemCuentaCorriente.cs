using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class ItemCuentaCorriente : INotifyPropertyChanged
    {
        private int id;
        private int idVenta;
        private int idTipoCC;
        private string detalle;
        private float monto;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IdVenta
        {
            get { return idVenta; }
        }

        public int IdTipoCC
        {
            get { return idTipoCC; }
            set { idTipoCC = value; OnPropertyChanged("IdTipoCC"); }
        }

        public string TipoCC
        {
            get { return Modelo.TipoCCList.getInstance()[idTipoCC - 1]; }
            set { idTipoCC = Modelo.TipoCCList.index(value); OnPropertyChanged("IdTipoCC"); }
        }

        public string Detalle
        {
            get { return detalle; }
            set { detalle = value; OnPropertyChanged("Detalle"); }
        }

        public float Monto
        {
            get { return monto; }
            set { monto = value; OnPropertyChanged("Monto"); }
        }

        public ItemCuentaCorriente(int id, int idVenta, int idTipoCC, string detalle, float monto)
        {
            this.id = id;
            this.idVenta = idVenta;
            this.idTipoCC = idTipoCC;
            this.detalle = detalle;
            this.monto = monto;
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
