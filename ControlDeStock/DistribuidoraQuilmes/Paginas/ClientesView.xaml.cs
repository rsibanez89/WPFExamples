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
using DistribuidoraQuilmes.ConexionBase;
using DistribuidoraQuilmes.Modelo;


namespace DistribuidoraQuilmes.Paginas
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class ClientesView : Page
    {
        private static ClientesView instance;
        private Clientes model_clientela;
        
        public ClientesView()
        {
            InitializeComponent(); 
            inicializarMisComponentes();
        }

        public static ClientesView getInstance()
        {
            if (instance == null)
                instance = new ClientesView();
            return instance;
        }

        public void inicializarMisComponentes()
        {
            model_clientela = new Clientes();
            this.dataGrid1.DataContext = model_clientela;
            this.ComboBoxColumn.ItemsSource = CiudadesList.getInstance();
        }

        private void bStock_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(StockDeProductos.getInstance());
        }

        private void bVentas_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new NuevoRepartoView());
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            model_clientela.addNewCliente();
        }

        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGrid1.SelectedIndex >= 0)
            {
                if (MessageBox.Show("¿Seguro que desea eliminar el cliente?", "Eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Cliente c = this.dataGrid1.SelectedItem as Cliente;
                    model_clientela.deleteCliente(c);
                }
            }
        }
    }
}
