using System.Windows;
using System.Data;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using DistribuidoraQuilmes.ConexionBase;
using DistribuidoraQuilmes.Paginas;

namespace DistribuidoraQuilmes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {

        public PageSwitcher()
        {
            InitializeComponent();
            Switcher.setPageSwitcher(this);
            Switcher.Switch(PageInicial.getInstance());
        }

    }
}
