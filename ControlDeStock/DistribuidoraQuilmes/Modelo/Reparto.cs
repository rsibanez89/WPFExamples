using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using DistribuidoraQuilmes.ConexionBase;

namespace DistribuidoraQuilmes.Modelo
{
    public class Reparto : ObservableCollection<Venta>
    {
        public static readonly string nombreTabla = "Ventas";

        private int id;
        private int idCiudad;
        private DateTime fecha;
        private int index;
        private bool ventasCargadas;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IdCiudad
        {
            get { return idCiudad; }
            set { idCiudad = value; }
        }

        public string Ciudad
        {
            get { return CiudadesList.getInstance()[idCiudad - 1]; }
            set { idCiudad = CiudadesList.index(value); }
        }

        public string Fecha
        {
            get { return fecha.ToString("dd/MM/yyyy"); }
            set { this.fecha = DateTime.Parse(value); }
        }

        public int Index
        {
            get { return index; }
            set
                {
                    if (value > 0 && value <= this.Count)
                    {
                        index = value;
                        base.OnPropertyChanged(new PropertyChangedEventArgs("Index"));
                    }
                }
        }

        public Reparto(int id, int idCiudad, DateTime fecha) : base()
        {
            this.id = id;
            this.idCiudad = idCiudad;
            this.fecha = fecha;
            index = 0;
            ventasCargadas = false;
        }

        public void cargarVentas()
        {
            if (!ventasCargadas)
            {
                DataSet dataSet = MiddleDBAccess.getDataset(nombreTabla, "idreparto", id.ToString());

                Clientes listaDeClientes = new Clientes(this.idCiudad);

                foreach (DataRow pRow in dataSet.Tables[nombreTabla].Rows)
                {
                    int idventa = System.Convert.ToInt32(pRow["id"]);
                    int idcliente = System.Convert.ToInt32(pRow["idcliente"]);
                    Venta v = new Venta(idventa, listaDeClientes.clienteAt(idcliente), this);
                    this.Add(v);
                }
                ventasCargadas = true;
            }
        }

        public Venta addNewVenta(Cliente cliente, Reparto reparto)
        {
            Venta v = MiddleDBAccess.addNewVenta(cliente, reparto);
            this.Add(v);
            return v;
        }
    }
}
