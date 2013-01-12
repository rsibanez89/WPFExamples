using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DistribuidoraQuilmes
{
    public static class Switcher
    {
        private static PageSwitcher instance;

        public static void setPageSwitcher(PageSwitcher pageSwitcher)
        {
            instance = pageSwitcher;
        }

        public static void Switch(Page nextPage)
        {
            instance.Content = nextPage;
        }
    }
}
