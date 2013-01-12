using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Data;
using System.ComponentModel;
using DistribuidoraQuilmes.ConexionBase;
using System.Reflection;

namespace DistribuidoraQuilmes.Modelo
{
    class Clientes : ObservableCollection<Cliente>
    {

        public static readonly string nombreTabla = "Clientes";

        public Clientes() : base()
        {
            DataSet dataSet = MiddleDBAccess.getDataset(nombreTabla);
            cargarClientes(dataSet);
        }

        public Clientes(int idCiudad) : base()
        {
            DataSet dataSet = MiddleDBAccess.getDataset(nombreTabla, "idciudad", idCiudad.ToString());
            cargarClientes(dataSet);
        }

        private void cargarClientes(DataSet dataSet)
        {
            foreach (DataRow pRow in dataSet.Tables[nombreTabla].Rows)
            {
                int id = System.Convert.ToInt32(pRow["id"]);
                string apellido = pRow["apellido"].ToString();
                string nombre = pRow["nombre"].ToString();
                string razonSocial = pRow["razonSocial"].ToString();
                string telefono = pRow["telefono"].ToString();
                string movil = pRow["movil"].ToString();
                int idCiudad = System.Convert.ToInt32(pRow["idciudad"]);

                Cliente c = new Cliente(id, apellido, nombre, razonSocial, telefono, movil, idCiudad);
                c.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
                this.Add(c);
            }
        }

        public void addNewCliente()
        {
            Cliente c = MiddleDBAccess.addNewCliente();
            c.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(propiedadCambiada);
            this.Add(c);
        }

        public void deleteCliente(Cliente c)
        {
            MiddleDBAccess.remove(nombreTabla, c.ID);
            this.Remove(c);
        }

        private void propiedadCambiada(object sender, PropertyChangedEventArgs info)
        {
            Cliente c = (Cliente)sender;
            object value = sender.GetType().GetProperty(info.PropertyName).GetValue(sender, null);
            MiddleDBAccess.update(nombreTabla, c.ID, info.PropertyName, value);
        }

        public Cliente clienteAt(int idCliente)
        {
            for (int i = 0; i < this.Count; i++)
                if (this[i].ID == idCliente)
                    return this[i];
            return null;
        }
    }
}
