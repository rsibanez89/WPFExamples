using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DistribuidoraQuilmes.Modelo;
using DistribuidoraQuilmes.ConexionBase;

namespace DistribuidoraQuilmes.Paginas
{
    /// <summary>
    /// Lógica de interacción para PageRepartoView.xaml
    /// </summary>
    public partial class PageVentaView : Page
    {
        private PagePlanillaDeCarga model_planilla_de_carga;
        private Reparto model_reparto;
        private Clientes model_clientes;
        private List<Cliente> clientesMarcados;

        public PageVentaView(Reparto model_reparto)
        {
            InitializeComponent();
            this.model_reparto = model_reparto;
            inicializarMisComponentes();
            model_reparto.Index = 0;
        }

        public void inicializarMisComponentes()
        {
            clientesMarcados = new List<Cliente>();
            model_clientes = new Clientes(model_reparto.IdCiudad);

            //si ya hay una venta abierta para ese cliente, pasarlo a clinetes marcados.
            for(int i = 0; i < model_reparto.Count; i++)
            {
                int idcliente = model_reparto[i].IdCliente;
                for(int j = 0; j < model_clientes.Count; j++)
                    if (model_clientes[j].ID == idcliente)
                    {
                        clientesMarcados.Add(model_clientes[j]);
                        model_clientes.RemoveAt(j);
                        break;
                    }
            }
            
            wrapPanel1.DataContext = model_reparto;
            wrapPanel2.DataContext = model_clientes;
            wrapPanel3.DataContext = model_reparto;
            ComboBoxColumn.ItemsSource = TipoCCList.getInstance();
            if (model_reparto.Count > 0)
            {
                model_reparto.Index = model_reparto.Count;
                updateDataContext();
            }
        }

        private void bAddVenta_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                model_reparto.addNewVenta(model_clientes[comboBox1.SelectedIndex], model_reparto);
                model_reparto.Index = model_reparto.Count;

                clientesMarcados.Add(model_clientes[comboBox1.SelectedIndex]);
                model_clientes.RemoveAt(comboBox1.SelectedIndex);
                updateDataContext();
            }
        }

        private void bAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (model_reparto.Index > 1)
            {
                --model_reparto.Index;
                updateDataContext();
            }
        }

        private void bSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (model_reparto.Index < model_reparto.Count)
            {
                ++model_reparto.Index;
                updateDataContext();
            }
        }

        private void updateDataContext()
        {
            wrapPanel4.DataContext = clientesMarcados[model_reparto.Index - 1];
            wrapPanel5.DataContext = clientesMarcados[model_reparto.Index - 1];
            wrapPanel6.DataContext = clientesMarcados[model_reparto.Index - 1];
            wrapPanel7.DataContext = model_reparto[model_reparto.Index - 1];
            dataGrid1.DataContext = model_reparto[model_reparto.Index - 1];
            dataGrid2.DataContext = model_reparto[model_reparto.Index - 1].CuentaCorriente;
        }

        private void bImprimir_Click(object sender, RoutedEventArgs e)
        {
            if(model_reparto.Count > 0)
                MiddleImpresora.ImprimirVenta(clientesMarcados[model_reparto.Index - 1], model_reparto[model_reparto.Index - 1], model_reparto.Fecha);
        }

        private void bInicio_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(PageInicial.getInstance());
        }

        private void bVerPlanillaDeCarga_Click(object sender, RoutedEventArgs e)
        {
            if (model_planilla_de_carga == null)
                model_planilla_de_carga = new PagePlanillaDeCarga(model_reparto, this);
            model_planilla_de_carga.actualizarPlanilla();
            Switcher.Switch(model_planilla_de_carga);
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            if (model_reparto.Count > 0)
                model_reparto[model_reparto.Index - 1].CuentaCorriente.addNewItemCuentaCorriente();
        }

        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGrid2.SelectedIndex >= 0)
            {
                if (MessageBox.Show("¿Seguro que desea eliminar el recibo?", "Eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ItemCuentaCorriente item = this.dataGrid2.SelectedItem as ItemCuentaCorriente;
                    model_reparto[model_reparto.Index - 1].CuentaCorriente.deleteItemCuentaCorriente(item);
                }
            }
        }
    }
}
