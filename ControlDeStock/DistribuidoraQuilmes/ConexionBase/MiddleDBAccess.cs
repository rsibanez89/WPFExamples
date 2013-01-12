using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using DistribuidoraQuilmes.Modelo;

namespace DistribuidoraQuilmes.ConexionBase
{
    public class MiddleDBAccess
    {
        //Creo la cadena de conexion para Office 2007
        public static readonly string cadena = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb;Persist Security Info=False";

        public static DataSet getDataset(string nombreTabla)
        {
            //Objeto conexion
            OleDbConnection conexion = new OleDbConnection(cadena);

            //Creo el adaptador y selecciono los datos de la tabla
            OleDbDataAdapter adapClientes = new OleDbDataAdapter("SELECT * FROM " + nombreTabla, conexion);

            //Creo el DataSet
            DataSet dataSet = new DataSet();
            //Relleno el adaptador con los datos en memoria
            adapClientes.Fill(dataSet, nombreTabla);

            return dataSet;
        }

        public static DataSet getDataset(string nombreTabla, string propiedad, string value)
        {
            //Objeto conexion
            OleDbConnection conexion = new OleDbConnection(cadena);

            //Creo el adaptador y selecciono los datos de la tabla
            OleDbDataAdapter adapClientes = new OleDbDataAdapter("SELECT * FROM " + nombreTabla + " WHERE " + propiedad + " = " + value, conexion);

            //Creo el DataSet
            DataSet dataSet = new DataSet();
            //Relleno el adaptador con los datos en memoria
            adapClientes.Fill(dataSet, nombreTabla);

            return dataSet;
        }

        public static Cliente addNewCliente()
        {
            string str = "INSERT INTO Clientes (apellido, nombre, razonSocial, telefono, movil, idciudad) VALUES (?, ?, ?, ?, ?, ?)";
            string query = "Select @@Identity";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    string apellido = "";
                    string nombre = "";
                    string razonSocial = "";
                    string telefono = "";
                    string movil = "";
                    int idciudad = 1;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("apellido", apellido);
                    cmd.Parameters.AddWithValue("nombre", nombre);
                    cmd.Parameters.AddWithValue("razonsocial", razonSocial);
                    cmd.Parameters.AddWithValue("telefono", telefono);
                    cmd.Parameters.AddWithValue("movil", movil);
                    cmd.Parameters.AddWithValue("ciudad", idciudad);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query;
                    int id = (int)cmd.ExecuteScalar();
                    return new Cliente(id, apellido, nombre, razonSocial, telefono, movil, idciudad);
                }
            }
        }

        public static Producto addNewProducto()
        {
            string str = "INSERT INTO Productos (codigo, detalle, idretornable, precio, stock, fechamodificado) VALUES (?, ?, ?, ?, ?, ?)";
            string query = "Select @@Identity";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    int codigo = 999999;
                    string detalle = "DETALLE";
                    int idRetornable = 1;
                    float precio = 0;
                    int stock = 0;
                    DateTime now = DateTime.Now;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("codigo", codigo);
                    cmd.Parameters.AddWithValue("detalle", detalle);
                    cmd.Parameters.AddWithValue("idretornable", idRetornable);
                    cmd.Parameters.AddWithValue("precio", precio);
                    cmd.Parameters.AddWithValue("stock", stock);
                    cmd.Parameters.AddWithValue("fechamodificado", now.ToString("dd/MM/yyyy"));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query;
                    int id = (int)cmd.ExecuteScalar();
                    return new Producto(id, codigo, detalle, idRetornable, precio, stock, now);
                }
            }
        }

        public static Reparto addNewReparto(int idCiudad, DateTime fecha)
        {
            string str = "INSERT INTO Repartos (idciudad, fecha) VALUES (?, ?)";
            string query = "Select @@Identity";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("idciudad", idCiudad);
                    cmd.Parameters.AddWithValue("fecha", fecha);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query;
                    int id = (int)cmd.ExecuteScalar();
                    return new Reparto(id, idCiudad, fecha);
                }
            }
        }

        public static Venta addNewVenta(Cliente cliente, Reparto reparto)
        {
            string str = "INSERT INTO Ventas (idcliente, idreparto) VALUES (?, ?)";
            string query = "Select @@Identity";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("idcliente", cliente.ID);
                    cmd.Parameters.AddWithValue("idreparto", reparto.ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query;
                    int id = (int)cmd.ExecuteScalar();
                    return new Venta(id, cliente, reparto);
                }
            }
        }

        public static void addNewItem(ItemVenta item)
        {
            string str = "INSERT INTO ItemVenta (idventa, idproducto, cantidad, precio) VALUES (?, ?, ?, ?)";
            string query = "Select @@Identity";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("idventa", item.IdVenta);
                    cmd.Parameters.AddWithValue("idproducto", item.IdProducto);
                    cmd.Parameters.AddWithValue("cantidad", item.Cantidad);
                    cmd.Parameters.AddWithValue("precio", item.Precio);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query;
                    item.ID = (int)cmd.ExecuteScalar();
                }
            }
        }

        public static void remove(string nombreTabla, int ID)
        {
            string str = "DELETE FROM " + nombreTabla + " WHERE Id = ?";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("Id", ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void update(string nombreTabla, int ID, string propiedad, object value)
        {
            string str = "UPDATE " + nombreTabla + " SET " + propiedad + "= ? WHERE Id = ?";

            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue(propiedad, value);
                    cmd.Parameters.AddWithValue("Id", ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void addNewItemPlanillaDeCarga(ItemPlanillaDeCarga item)
        {
            string str = "INSERT INTO EntraProducto (idreparto, idproducto, cantidad) VALUES (?, ?, ?)";
            string query = "Select @@Identity";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("idreparto", item.IdReparto);
                    cmd.Parameters.AddWithValue("idproducto", item.IdProducto);
                    cmd.Parameters.AddWithValue("cantidad", item.Entra);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query;
                    item.ID = (int)cmd.ExecuteScalar();
                }
            }
        }

        public static void addNewItemRetornablePlanillaDeCarga(ItemRetornablePlanillaDeCarga item)
        {
            string str = "INSERT INTO EntraRetornable (idreparto, idretornable, cantidad) VALUES (?, ?, ?)";
            string query = "Select @@Identity";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("idreparto", item.IdReparto);
                    cmd.Parameters.AddWithValue("idretornable", item.IdRetornable);
                    cmd.Parameters.AddWithValue("cantidad", item.Entra);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query;
                    item.ID = (int)cmd.ExecuteScalar();
                }
            }
        }

        public static void addNewItemCuentaCorriente(ItemCuentaCorriente item)
        {
            string str = "INSERT INTO CuentaCorriente (idventa, idtipocc, detalle, monto) VALUES (?, ?, ?, ?)";
            string query = "Select @@Identity";
            using (OleDbConnection con = new OleDbConnection(cadena))
            {
                using (OleDbCommand cmd = new OleDbCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("idventa", item.IdVenta);
                    cmd.Parameters.AddWithValue("idtipocc", item.IdTipoCC);
                    cmd.Parameters.AddWithValue("detalle", item.Detalle);
                    cmd.Parameters.AddWithValue("monto", item.Monto);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query;
                    item.ID = (int)cmd.ExecuteScalar();
                }
            }
        }

    }
}
