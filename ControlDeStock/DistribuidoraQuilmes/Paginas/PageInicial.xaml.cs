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

namespace DistribuidoraQuilmes.Paginas
{
    /// <summary>
    /// Lógica de interacción para PageInicial.xaml
    /// </summary>
    public partial class PageInicial : Page
    {
        private static PageInicial instance;

        private PageInicial()
        {
            InitializeComponent();
        }

        public static PageInicial getInstance()
        {
            if (instance == null)
                instance = new PageInicial();
            return instance;
        }

        private void bStock_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(StockDeProductos.getInstance());
        }

        private void bClientes_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new ClientesView());
        }

        private void bVentas_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new NuevoRepartoView());
        }  
    }
}
