using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSitek.Common
{
    internal interface SortMethodsLinq
    {
        ObservableCollection<ResponsibleEmployee> Sort(ObservableCollection<ResponsibleEmployee> data);
    }
}
