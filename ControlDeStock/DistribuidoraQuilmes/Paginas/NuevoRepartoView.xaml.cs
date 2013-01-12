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
    /// Lógica de interacción para NuevaVenta.xaml
    /// </summary>
    public partial class NuevoRepartoView : Page
    {
        private Repartos model_repartos;

        public NuevoRepartoView()
        {
            InitializeComponent(); 
            inicializarMisComponentes();
        }

        public void inicializarMisComponentes()
        {
            this.comboBox1.ItemsSource = CiudadesList.getInstance();
            model_repartos = Repartos.getInstance();
            this.dataGrid1.DataContext = model_repartos;
            this.ComboBoxColumn.ItemsSource = CiudadesList.getInstance();
        }

        private void bClientes_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(ClientesView.getInstance());
        }

        private void bStock_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(StockDeProductos.getInstance());
        }

        private void bAddVenta_Click(object sender, RoutedEventArgs e)
        {
            int idCiudad = this.comboBox1.SelectedIndex;
            if (idCiudad != -1 && this.datePicker1.SelectedDate != null)
            {
                Reparto nuevoReparto = model_repartos.addNewReparto(idCiudad + 1, this.datePicker1.SelectedDate.Value);
                nuevoReparto.cargarVentas();
                Switcher.Switch(new PageVentaView(nuevoReparto));
            }
            else
                MessageBox.Show("Debe ingresar lugar y fecha!!!", "Faltan datos", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void bEditarReparto_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedIndex != -1)
            {
                Reparto seleccionado = (Reparto)this.dataGrid1.SelectedItem;
                seleccionado.cargarVentas();
                Switcher.Switch(new PageVentaView(seleccionado));
            }
        }

        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGrid1.SelectedIndex >= 0)
            {
                if (MessageBox.Show("¿Seguro que desea eliminar el reparto?", "Eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Reparto r = this.dataGrid1.SelectedItem as Reparto;
                    model_repartos.deleteReparto(r);
                }
            }
        }
    }
}
