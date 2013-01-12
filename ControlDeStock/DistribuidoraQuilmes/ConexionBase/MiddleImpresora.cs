using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows;
using System.Data;
using System.Windows.Media;
using DistribuidoraQuilmes.Paginas;
using System.Windows.Xps;

namespace DistribuidoraQuilmes.ConexionBase
{
    public class MiddleImpresora
    {

        private static Table getEncabezadoNotaDePedido(Modelo.Cliente c, int idVenta, string fecha, bool original)
        {
            Table descripcion = new Table();
            descripcion.CellSpacing = 0;
            descripcion.BorderBrush = Brushes.Black;
            descripcion.BorderThickness = new Thickness(1.0);

            TableRow headerRow1 = new TableRow();
            headerRow1.FontWeight = FontWeights.Bold;

            headerRow1.Cells.Add(new TableCell());
            headerRow1.Cells.Add(new TableCell());

            Paragraph p = new Paragraph();
            p.Inlines.Add("Nota de pedido Nº " + idVenta);
            TableCell notaDePedido = new TableCell(p);
            notaDePedido.TextAlignment = TextAlignment.Center;
            notaDePedido.ColumnSpan = 2;
            notaDePedido.BorderBrush = Brushes.Black;
            notaDePedido.BorderThickness = new Thickness(1, 0, 1, 1);
            notaDePedido.Padding = new Thickness(5);
            headerRow1.Cells.Add(notaDePedido);

            headerRow1.Cells.Add(new TableCell());
            headerRow1.Cells.Add(new TableCell());

            TableRow headerRow2 = new TableRow();
            headerRow2.FontWeight = FontWeights.Bold;
            p = new Paragraph();
            p.Inlines.Add("Fecha: " + fecha);
            TableCell cfecha = new TableCell(p);
            cfecha.ColumnSpan = 3;
            cfecha.BorderBrush = Brushes.Black;
            cfecha.BorderThickness = new Thickness(0, 0, 1, 0);
            headerRow2.Cells.Add(cfecha);
            p = new Paragraph();
            p.Inlines.Add("FORMULARIO SIN VALOR FISCAL");
            TableCell vfiscal = new TableCell(p);
            vfiscal.ColumnSpan = 3;
            vfiscal.TextAlignment = TextAlignment.Center;
            headerRow2.Cells.Add(vfiscal);

            TableRow headerRow3 = new TableRow();
            headerRow3.FontWeight = FontWeights.Bold;
            p = new Paragraph();
            p.Inlines.Add("Lugar: " + c.Ciudad);
            TableCell lugar = new TableCell(p);
            lugar.ColumnSpan = 3;
            lugar.BorderBrush = Brushes.Black;
            lugar.BorderThickness = new Thickness(0, 0, 1, 0);
            headerRow3.Cells.Add(lugar);
            p = new Paragraph();
            if (original)
                p.Inlines.Add("ORIGINAL");
            else
                p.Inlines.Add("DUPLICADO");
            TableCell tipo = new TableCell(p);
            tipo.ColumnSpan = 3;
            tipo.TextAlignment = TextAlignment.Center;
            headerRow3.Cells.Add(tipo);

            TableRow headerRow4 = new TableRow();
            headerRow4.FontWeight = FontWeights.Bold;
            p = new Paragraph();
            p.Inlines.Add("Cliente: " + c.NombreCompleto);
            TableCell nombre = new TableCell(p);
            nombre.ColumnSpan = 3;
            nombre.BorderBrush = Brushes.Black;
            nombre.BorderThickness = new Thickness(0, 0, 1, 0);
            headerRow4.Cells.Add(nombre);
            p = new Paragraph();
            p.Inlines.Add("CORRESPONDE HACER FACTURA TIPO:");
            TableCell facturaTipo = new TableCell(p);
            facturaTipo.ColumnSpan = 3;
            facturaTipo.TextAlignment = TextAlignment.Center;
            headerRow4.Cells.Add(facturaTipo);


            TableRowGroup headerRowGroup = new TableRowGroup();
            headerRowGroup.Rows.Add(headerRow1);
            headerRowGroup.Rows.Add(headerRow2);
            headerRowGroup.Rows.Add(headerRow3);
            headerRowGroup.Rows.Add(headerRow4);

            descripcion.RowGroups.Add(headerRowGroup);
            return descripcion;
        }

        private static TableCell getTableCell(string texto)
        {
            Paragraph p = new Paragraph();
            p.Inlines.Add(texto);
            TableCell celda = new TableCell(p);
            return celda;
        }

        private static Table getTableHeaderNotaDePedido(bool original)
        {
            Table header = new Table();
            header.FontWeight = FontWeights.Bold;
            header.BorderBrush = Brushes.Black;
            header.BorderThickness = new Thickness(0, 0, 0, 2);

            TableRow headerRow = new TableRow();
            if (!original)
                headerRow.Cells.Add(getTableCell("Código"));
            TableCell producto = getTableCell("Producto");
            producto.ColumnSpan = 2;
            headerRow.Cells.Add(producto);
            headerRow.Cells.Add(new TableCell());
            headerRow.Cells.Add(new TableCell());
            headerRow.Cells.Add(getTableCell("Cant."));
            headerRow.Cells.Add(getTableCell("Unit."));
            headerRow.Cells.Add(getTableCell("Subtotal"));

            TableRowGroup headerRowGroup = new TableRowGroup();
            headerRowGroup.Rows.Add(headerRow);
            header.RowGroups.Add(headerRowGroup);
            return header;
        }

        private static TableRow getTableRowNotaDePedido(Modelo.Venta venta, int indice, bool original)
        {
            TableRow row = new TableRow();
            if (!original)
                row.Cells.Add(getTableCell(venta[indice].Codigo.ToString()));
            TableCell producto = getTableCell(venta[indice].Detalle);
            producto.ColumnSpan = 2;
            row.Cells.Add(producto);
            row.Cells.Add(new TableCell());
            row.Cells.Add(new TableCell());
            row.Cells.Add(getTableCell(venta[indice].Cantidad.ToString()));
            row.Cells.Add(getTableCell(venta[indice].Precio.ToString()));
            row.Cells.Add(getTableCell(venta[indice].Subtotal.ToString()));
            return row;
        }

        private static TableRow getWhiteTableRow()
        {
            TableRow row = new TableRow();
            TableCell white = getTableCell("  ");
            row.Cells.Add(white);
            return row;
        }

        private static Table getDescripcionNotaDePedido(Modelo.Venta venta, bool original)
        {
            Table table = new Table();
            TableRowGroup fila = new TableRowGroup();
            for (int i = 0; i < venta.Count; i++)
                if (venta[i].Cantidad != 0)
                    fila.Rows.Add(getTableRowNotaDePedido(venta, i, original));
            for (int i = fila.Rows.Count; i < 34; i++)
                fila.Rows.Add(getWhiteTableRow());
            table.RowGroups.Add(fila);

            table.FontSize = 9;
            table.BorderBrush = Brushes.Black;
            table.BorderThickness = new Thickness(0, 0, 0, 2);

            return table;
        }

        private static Table getLine()
        {
            Table table = new Table();
            table.BorderBrush = Brushes.Black;
            table.BorderThickness = new Thickness(0, 0, 0, 2);
            TableRowGroup fila = new TableRowGroup();
            TableRow columna = new TableRow();
            columna.Cells.Add(new TableCell());
            fila.Rows.Add(columna);
            table.RowGroups.Add(fila);
            return table;
        }

        private static Table getTotalVenta(Modelo.Venta venta)
        {
            Table table = new Table();
            TableRowGroup fila = new TableRowGroup();
            TableRow columna = new TableRow();
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            TableCell total = getTableCell("TOTAL: ");
            total.TextAlignment = TextAlignment.Right;
            columna.Cells.Add(total);
            TableCell valor = getTableCell(venta.Total.ToString());
            valor.TextAlignment = TextAlignment.Center;
            valor.BorderBrush = Brushes.Black;
            valor.BorderThickness = new Thickness(1.0);
            columna.Cells.Add(valor);
            fila.Rows.Add(columna);
            table.RowGroups.Add(fila);
            return table;
        }

        private static void DoThePrint(System.Windows.Documents.FlowDocument document)
        {
            LocalPrintServer ps = new LocalPrintServer();
            // Get the default print queue
            PrintQueue pq = ps.DefaultPrintQueue;
            // Get an XpsDocumentWriter for the default print queue
            XpsDocumentWriter xpsdw = PrintQueue.CreateXpsDocumentWriter(pq);

            PrintCapabilities printCapabilites = pq.GetPrintCapabilities();
            PrintTicket mySettings = new PrintTicket();
            mySettings.PageMediaSize = getOficioCapabilities(printCapabilites);

            System.Printing.ValidationResult result = pq.MergeAndValidatePrintTicket(pq.UserPrintTicket, mySettings);

            // check if our settings got applied in the ValidatedPrintTicket of results...
            if (result.ValidatedPrintTicket.PageMediaSize.PageMediaSizeName == printCapabilites.PageMediaSizeCapability[3].PageMediaSizeName)
            {
                // yes, so set result in the PrintQueue and Commit. 
                pq.UserPrintTicket = result.ValidatedPrintTicket;
                pq.Commit();
            }

            if (xpsdw != null)
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;

                paginator.PageSize = new Size(mySettings.PageMediaSize.Width.Value, mySettings.PageMediaSize.Height.Value);
                Thickness t = new Thickness(72);
                document.PagePadding = t;

                document.ColumnWidth = double.PositiveInfinity;
                xpsdw.Write(paginator);
            }
        }

        private static PageMediaSize getOficioCapabilities(PrintCapabilities pc)
        {
            foreach (PageMediaSize mediaSize in pc.PageMediaSizeCapability)
            {
                if (mediaSize.PageMediaSizeName == PageMediaSizeName.NorthAmericaLegal)
                {
                    return mediaSize;
                }
            }
            return null;
        }

        private static void DoThePrint2(System.Windows.Documents.FlowDocument document)
        {
            // Create a XpsDocumentWriter object, implicitly opening a Windows common print dialog,
            // and allowing the user to select a printer.

            // get information about the dimensions of the seleted printer+media.
            System.Printing.PrintDocumentImageableArea ia = null;
            System.Windows.Xps.XpsDocumentWriter docWriter = System.Printing.PrintQueue.CreateXpsDocumentWriter(ref ia);

            if (docWriter != null && ia != null)
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;

                // Change the PageSize and PagePadding for the document to match the CanvasSize for the printer device.
                paginator.PageSize = new Size(ia.MediaSizeWidth, ia.MediaSizeHeight);
                Thickness t = new Thickness(72);  // copy.PagePadding;
                document.PagePadding = new Thickness(Math.Max(ia.OriginWidth, t.Left), Math.Max(ia.OriginHeight, t.Top), Math.Max(ia.MediaSizeWidth - (ia.OriginWidth + ia.ExtentWidth), t.Right), Math.Max(ia.MediaSizeHeight - (ia.OriginHeight + ia.ExtentHeight), t.Bottom));

                document.ColumnWidth = double.PositiveInfinity;
                //copy.PageWidth = 528; // allow the page to be the natural with of the output device

                // Send content to the printer.
                docWriter.Write(paginator);
            }
        }

        public static void ImprimirVenta(Modelo.Cliente cliente, Modelo.Venta venta, string fecha)
        {
            // Create a FlowDocument dynamically.
            FlowDocument doc = new FlowDocument();
            //doc.Name = "Documento de Pedido";

            doc.Blocks.Add(getEncabezadoNotaDePedido(cliente, venta.ID, fecha, true));
            doc.Blocks.Add(getTableHeaderNotaDePedido(true));
            doc.Blocks.Add(getDescripcionNotaDePedido(venta, true));

            doc.Blocks.Add(getTotalVenta(venta));

            doc.Blocks.Add(getLine());

            doc.Blocks.Add(getEncabezadoNotaDePedido(cliente, venta.ID, fecha, false));
            doc.Blocks.Add(getTableHeaderNotaDePedido(false));
            doc.Blocks.Add(getDescripcionNotaDePedido(venta, false));
            doc.Blocks.Add(getTotalVenta(venta));
            doc.FontFamily = new FontFamily("Arial");
            doc.FontSize = 12;

            /*ImpresionDePedido i = new ImpresionDePedido();
            i.Content = doc;
            i.Show();*/

            DoThePrint(doc);
        }

        private static Table getEncabezadoPlanillaDeCarga(Modelo.PlanillaDeCarga planilla)
        {
            Table descripcion = new Table();
            descripcion.CellSpacing = 0;
            descripcion.BorderBrush = Brushes.Black;
            descripcion.BorderThickness = new Thickness(1);

            TableRow headerRow1 = new TableRow();
            headerRow1.FontWeight = FontWeights.Bold;

            headerRow1.Cells.Add(new TableCell());

            Paragraph p = new Paragraph();
            p.Inlines.Add("Planilla de carga Nº " + planilla.Repart.ID);
            TableCell planillaDeCarga = new TableCell(p);
            planillaDeCarga.TextAlignment = TextAlignment.Center;
            planillaDeCarga.BorderBrush = Brushes.Black;
            planillaDeCarga.BorderThickness = new Thickness(1, 0, 1, 1);
            planillaDeCarga.Padding = new Thickness(5);
            headerRow1.Cells.Add(planillaDeCarga);

            headerRow1.Cells.Add(new TableCell());

            TableRow headerRow2 = new TableRow();
            headerRow2.FontWeight = FontWeights.Bold;

            p = new Paragraph();
            p.Inlines.Add("Fecha: " + planilla.Repart.Fecha);
            TableCell cfecha = new TableCell(p);
            cfecha.TextAlignment = TextAlignment.Center;
            headerRow2.Cells.Add(cfecha);
            p = new Paragraph();

            p = new Paragraph();
            p.Inlines.Add("Lugar: " + planilla.Repart.Ciudad);
            TableCell lugar = new TableCell(p);
            lugar.TextAlignment = TextAlignment.Center;
            lugar.BorderBrush = Brushes.Black;
            lugar.BorderThickness = new Thickness(1, 0, 1, 0);
            headerRow2.Cells.Add(lugar);

            int remitoInicial = 0;
            int remitoFinal = 0;
            if (planilla.Repart.Count > 0)
            {
                remitoInicial = planilla.Repart[0].ID;
                remitoFinal = planilla.Repart[planilla.Repart.Count - 1].ID;
            }

            p = new Paragraph();
            p.Inlines.Add("Remitos del " + remitoInicial + " al " + remitoFinal);
            TableCell remitos = new TableCell(p);
            remitos.TextAlignment = TextAlignment.Center;
            headerRow2.Cells.Add(remitos);

            TableRowGroup headerRowGroup = new TableRowGroup();
            headerRowGroup.Rows.Add(headerRow1);
            headerRowGroup.Rows.Add(headerRow2);

            descripcion.RowGroups.Add(headerRowGroup);
            return descripcion;
        }

        private static Table getTableHeaderPlanillaDeCarga()
        {
            Table header = new Table();
            header.FontWeight = FontWeights.Bold;
            header.BorderBrush = Brushes.Black;
            header.BorderThickness = new Thickness(0, 0, 0, 2);

            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(getTableCell("Código"));
            TableCell producto = getTableCell("Producto");
            producto.ColumnSpan = 2;
            headerRow.Cells.Add(producto);
            headerRow.Cells.Add(new TableCell());
            headerRow.Cells.Add(getTableCell("Sale"));
            headerRow.Cells.Add(getTableCell("Entra"));
            headerRow.Cells.Add(getTableCell("Venta"));
            headerRow.Cells.Add(getTableCell("Unit."));
            headerRow.Cells.Add(getTableCell("Subtotal"));

            TableRowGroup headerRowGroup = new TableRowGroup();
            headerRowGroup.Rows.Add(headerRow);
            header.RowGroups.Add(headerRowGroup);
            return header;
        }

        private static TableRow getTableRowPlanillaDeCarga(Modelo.PlanillaDeCarga planilla, int indice)
        {
            TableRow row = new TableRow();
            row.Cells.Add(getTableCell(planilla[indice].Codigo.ToString()));
            TableCell producto = getTableCell(planilla[indice].Detalle);
            producto.ColumnSpan = 2;
            row.Cells.Add(producto);
            row.Cells.Add(new TableCell());
            row.Cells.Add(getTableCell(planilla[indice].Sale.ToString()));
            row.Cells.Add(getTableCell(planilla[indice].Entra.ToString()));
            row.Cells.Add(getTableCell(planilla[indice].Venta.ToString()));
            row.Cells.Add(getTableCell(planilla[indice].Precio.ToString()));
            row.Cells.Add(getTableCell(planilla[indice].Subtotal.ToString()));
            return row;
        }

        private static Table getDescripcionPlanillaDeCarga(Modelo.PlanillaDeCarga planilla)
        {
            Table table = new Table();
            TableRowGroup fila = new TableRowGroup();
            for (int i = 0; i < planilla.Count; i++)
                fila.Rows.Add(getTableRowPlanillaDeCarga(planilla, i));
            table.RowGroups.Add(fila);

            table.FontSize = 9;

            return table;
        }

        private static Table getTotalVentasPlanillaDeCarga(Modelo.PlanillaDeCarga planilla)
        {
            Table table = new Table();
            TableRowGroup fila = new TableRowGroup();
            TableRow columna = new TableRow();
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            TableCell total = getTableCell("Total Ventas: ");
            total.TextAlignment = TextAlignment.Right;
            columna.Cells.Add(total);
            TableCell valor = getTableCell(planilla.Total.ToString());
            columna.Cells.Add(valor);
            fila.Rows.Add(columna);
            table.RowGroups.Add(fila);
            return table;
        }

        private static Table getTableHeaderRetornablesPlanillaDeCarga()
        {
            Table header = new Table();
            header.FontWeight = FontWeights.Bold;
            header.BorderBrush = Brushes.Black;
            header.BorderThickness = new Thickness(0, 0, 0, 2);

            TableRow headerRow = new TableRow();
            TableCell producto = getTableCell("Retornables");
            producto.ColumnSpan = 2;
            headerRow.Cells.Add(producto);
            headerRow.Cells.Add(new TableCell());
            headerRow.Cells.Add(new TableCell());
            headerRow.Cells.Add(getTableCell("Sale"));
            headerRow.Cells.Add(getTableCell("Entra"));
            headerRow.Cells.Add(getTableCell("Venta"));
            headerRow.Cells.Add(new TableCell());
            headerRow.Cells.Add(new TableCell());

            TableRowGroup headerRowGroup = new TableRowGroup();
            headerRowGroup.Rows.Add(headerRow);
            header.RowGroups.Add(headerRowGroup);
            return header;
        }

        private static TableRow getTableRowRetornablePlanillaDeCarga(Modelo.RetornablePlanillaDeCarga retornables, int indice)
        {
            TableRow row = new TableRow();
            TableCell producto = getTableCell(retornables[indice].Detalle);
            producto.ColumnSpan = 2;
            row.Cells.Add(producto);
            row.Cells.Add(new TableCell());
            row.Cells.Add(new TableCell());
            row.Cells.Add(getTableCell(retornables[indice].Sale.ToString()));
            row.Cells.Add(getTableCell(retornables[indice].Entra.ToString()));
            row.Cells.Add(getTableCell(retornables[indice].Venta.ToString()));
            row.Cells.Add(new TableCell());
            row.Cells.Add(new TableCell());
            return row;
        }

        private static Table getDescripcionRetornablePlanillaDeCarga(Modelo.RetornablePlanillaDeCarga retornables)
        {
            Table table = new Table();
            TableRowGroup fila = new TableRowGroup();
            for (int i = 0; i < retornables.Count; i++)
                fila.Rows.Add(getTableRowRetornablePlanillaDeCarga(retornables, i));
            table.RowGroups.Add(fila);

            table.FontSize = 9;
            return table;
        }

        private static Table getTableHeaderCCPlanillaDeCarga()
        {
            Table header = new Table();
            header.FontWeight = FontWeights.Bold;
            header.BorderBrush = Brushes.Black;
            header.BorderThickness = new Thickness(0, 0, 0, 2);

            TableRow headerRow1 = new TableRow();
            TableCell cc = getTableCell("Cuentas Corrientes");
            cc.ColumnSpan = 2;
            headerRow1.Cells.Add(cc);

            TableRow headerRow2 = new TableRow();
            headerRow2.Cells.Add(getTableCell("Cliente"));
            TableCell detalle = getTableCell("Detalle");
            detalle.ColumnSpan = 2;
            headerRow2.Cells.Add(detalle);
            headerRow2.Cells.Add(new TableCell());
            headerRow2.Cells.Add(getTableCell("Importe Facturas"));
            headerRow2.Cells.Add(getTableCell("Importe Recibos"));

            TableRowGroup headerRowGroup = new TableRowGroup();
            headerRowGroup.Rows.Add(headerRow1);
            headerRowGroup.Rows.Add(headerRow2);
            header.RowGroups.Add(headerRowGroup);
            return header;
        }

        private static TableRow getTableRowCCPlanillaDeCarga(Modelo.CuentaCorrientePlanillaDeCarga cuentasCorrientes, int indice)
        {
            TableRow row = new TableRow();
            row.Cells.Add(getTableCell(cuentasCorrientes[indice].RazonSocial));
            TableCell detalle = getTableCell(cuentasCorrientes[indice].Detalle);
            detalle.ColumnSpan = 2;
            row.Cells.Add(detalle);
            row.Cells.Add(new TableCell());
            if (cuentasCorrientes[indice].Tipo == "Factura")
            {
                row.Cells.Add(getTableCell(cuentasCorrientes[indice].Monto.ToString()));
                row.Cells.Add(new TableCell());
            }
            else
            {
                row.Cells.Add(new TableCell());
                row.Cells.Add(getTableCell(cuentasCorrientes[indice].Monto.ToString()));
            }
            return row;
        }

        private static Table getDescripcionCCPlanillaDeCarga(Modelo.CuentaCorrientePlanillaDeCarga cuentasCorrientes)
        {
            Table table = new Table();
            TableRowGroup fila = new TableRowGroup();
            for (int i = 0; i < cuentasCorrientes.Count; i++)
                fila.Rows.Add(getTableRowCCPlanillaDeCarga(cuentasCorrientes, i));
            table.RowGroups.Add(fila);

            table.FontSize = 9;
            return table;
        }

        private static Table getTotalCCPlanillaDeCarga(Modelo.CuentaCorrientePlanillaDeCarga cuentasCorrientes)
        {
            Table table = new Table();
            TableRowGroup fila = new TableRowGroup();
            TableRow columna = new TableRow();
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            TableCell total = getTableCell("Total:  ");
            total.TextAlignment = TextAlignment.Right;
            columna.Cells.Add(total);
            columna.Cells.Add(getTableCell(cuentasCorrientes.getTotal("Factura").ToString()));
            columna.Cells.Add(getTableCell(cuentasCorrientes.getTotal("Recibo").ToString()));
            fila.Rows.Add(columna);
            table.RowGroups.Add(fila);
            return table;
        }

        private static Table getTotalPlanillaDeCarga(Modelo.PlanillaDeCarga planilla, Modelo.CuentaCorrientePlanillaDeCarga cuentasCorrientes)
        {
            Table table = new Table();
            TableRowGroup fila = new TableRowGroup();
            TableRow columna = new TableRow();
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            columna.Cells.Add(new TableCell());
            TableCell total = getTableCell("TOTAL EFECTIVO: ");
            total.TextAlignment = TextAlignment.Right;
            columna.Cells.Add(total);

            float cuenta = planilla.Total - cuentasCorrientes.getTotal("Factura") + cuentasCorrientes.getTotal("Recibo");

            TableCell valor = getTableCell(cuenta.ToString());
            valor.TextAlignment = TextAlignment.Center;
            valor.BorderBrush = Brushes.Black;
            valor.BorderThickness = new Thickness(1.0);
            columna.Cells.Add(valor);
            fila.Rows.Add(columna);
            table.RowGroups.Add(fila);
            return table;
        }

        public static void ImprimirPlanillaDeCarga(Modelo.PlanillaDeCarga planilla, Modelo.RetornablePlanillaDeCarga retornables, Modelo.CuentaCorrientePlanillaDeCarga cuentasCorrientes)
        {
            // Create a FlowDocument dynamically.
            FlowDocument doc = new FlowDocument();
            //doc.Name = "Documento de Pedido";

            doc.Blocks.Add(getEncabezadoPlanillaDeCarga(planilla));

            doc.Blocks.Add(getTableHeaderPlanillaDeCarga());
            doc.Blocks.Add(getDescripcionPlanillaDeCarga(planilla));
            doc.Blocks.Add(getTotalVentasPlanillaDeCarga(planilla));

            doc.Blocks.Add(getTableHeaderRetornablesPlanillaDeCarga());
            doc.Blocks.Add(getDescripcionRetornablePlanillaDeCarga(retornables));

            doc.Blocks.Add(getTableHeaderCCPlanillaDeCarga());
            doc.Blocks.Add(getDescripcionCCPlanillaDeCarga(cuentasCorrientes));
            doc.Blocks.Add(getTotalCCPlanillaDeCarga(cuentasCorrientes));

            doc.Blocks.Add(getTotalPlanillaDeCarga(planilla, cuentasCorrientes));

            doc.FontFamily = new FontFamily("Arial");
            doc.FontSize = 12;

            /*ImpresionDePedido i = new ImpresionDePedido();
            i.Content = doc;
            i.Show();*/

            DoThePrint(doc);
        }

    }
}
