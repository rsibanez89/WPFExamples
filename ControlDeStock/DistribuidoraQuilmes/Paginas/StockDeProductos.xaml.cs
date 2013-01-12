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

namespace DistribuidoraQuilmes.Paginas
{
    /// <summary>
    /// Lógica de interacción para StockDeProductos.xaml
    /// </summary>
    public partial class StockDeProductos : Page
    {
        private static StockDeProductos instance;
        private Productos model_stock;

        private StockDeProductos()
        {
            InitializeComponent();
            inicializarMisComponentes();
        }

        public static StockDeProductos getInstance()
        {
            if (instance == null)
                instance = new StockDeProductos();
            return instance;
        }

        public void inicializarMisComponentes()
        {
            model_stock = Productos.getInstance();
            this.dataGrid1.DataContext = model_stock;
            this.ComboBoxColumn.ItemsSource = Retornables.getInstance();
        }

        private void bClientes_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ClientesView());
        }

        private void bVentas_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new NuevoRepartoView());
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            model_stock.addNewProducto();
        }

        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGrid1.SelectedIndex >= 0)
            {
                if (MessageBox.Show("¿Seguro que desea eliminar el producto?", "Eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Producto p = this.dataGrid1.SelectedItem as Producto;
                    model_stock.deleteProducto(p);
                }
            }
        }

        
    }
}
