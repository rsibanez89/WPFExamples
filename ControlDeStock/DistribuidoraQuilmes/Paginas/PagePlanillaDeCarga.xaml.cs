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
    /// Lógica de interacción para PlanillaDeCarga.xaml
    /// </summary>
    public partial class PagePlanillaDeCarga : Page
    {
        private PageVentaView model_page_venta_view;
        private Reparto model_reparto;
        private PlanillaDeCarga model_planilla_de_reparto;
        private RetornablePlanillaDeCarga model_retornable_planilla_de_carga;
        private CuentaCorrientePlanillaDeCarga model_cc_planilla_de_carga;

        public PagePlanillaDeCarga(Reparto reparto, PageVentaView page_venta_view)
        {
            InitializeComponent();
            this.model_reparto = reparto;
            this.model_page_venta_view = page_venta_view;
            inicializarMisComponentes();
        }

        public void inicializarMisComponentes()
        {
            this.model_planilla_de_reparto = new PlanillaDeCarga(model_reparto);
            this.model_retornable_planilla_de_carga = new RetornablePlanillaDeCarga(model_reparto);
            this.model_cc_planilla_de_carga = new CuentaCorrientePlanillaDeCarga(model_reparto);
            this.wrapPanel1.DataContext = model_reparto;
            this.dataGrid1.DataContext = model_planilla_de_reparto;
            this.dataGrid2.DataContext = model_retornable_planilla_de_carga;
            this.dataGrid3.DataContext = model_cc_planilla_de_carga;
        }

        private void bInicio_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(PageInicial.getInstance());
        }

        private void bVenta_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(model_page_venta_view);
        }

        public void actualizarPlanilla()
        {
            model_planilla_de_reparto.actualizarPlanilla();
            model_retornable_planilla_de_carga.actualizarPlanilla();
            model_cc_planilla_de_carga.actualizarPlanilla();
        }

        private void bImprimir_Click(object sender, RoutedEventArgs e)
        {
            MiddleImpresora.ImprimirPlanillaDeCarga(model_planilla_de_reparto, model_retornable_planilla_de_carga, model_cc_planilla_de_carga);
        }
    }
}
