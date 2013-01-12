using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DistribuidoraQuilmes.Modelo
{
    public class TipoCCList: ObservableCollection<string>
    {
        private static TipoCCList instance;

        private TipoCCList()
        {
            this.Add("Factura");
            this.Add("Recibo");
        }

        public static TipoCCList getInstance()
        {
            if (instance == null)
                instance = new TipoCCList();
            return instance;
        }

        public static int index(string element)
        {
            return instance.IndexOf(element)+1;
        }
    }
}
