using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace DistribuidoraQuilmes.Modelo
{
    class CiudadesList : ObservableCollection<string>
    {
        private static CiudadesList instance;

        private CiudadesList()
        {
            this.Add("JNF");
            this.Add("Claraz");
            this.Add("Barker");
            this.Add("La Dulce");
        }

        public static CiudadesList getInstance()
        {
            if (instance == null)
                instance = new CiudadesList();
            return instance;
        }

        public static int index(string element)
        {
            return instance.IndexOf(element)+1;
        }
    }
}
