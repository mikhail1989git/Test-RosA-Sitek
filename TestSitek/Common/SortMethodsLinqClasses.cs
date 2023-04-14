using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSitek.Common
{
    internal class SortByFio : SortMethodsLinq
    {
        public ObservableCollection<ResponsibleEmployee> Sort(ObservableCollection<ResponsibleEmployee> data)
        {
            return new ObservableCollection<ResponsibleEmployee>(data.OrderBy(x=>x.FIO));
        }
    }

    internal class SortByRkk : SortMethodsLinq
    {
        public ObservableCollection<ResponsibleEmployee> Sort(ObservableCollection<ResponsibleEmployee> data)
        {
            return new ObservableCollection<ResponsibleEmployee>(data.OrderByDescending(x => x.RkkCount)
                                                                      .ThenByDescending(x => x.ApealCount));
        }
    }

    internal class SortByAppeal : SortMethodsLinq
    {
        public ObservableCollection<ResponsibleEmployee> Sort(ObservableCollection<ResponsibleEmployee> data)
        {
            return new ObservableCollection<ResponsibleEmployee>(data.OrderByDescending(x => x.ApealCount)
                                                                     .ThenByDescending(x => x.RkkCount));
        }
    }

    internal class SortByTotalCount : SortMethodsLinq
    {
        public ObservableCollection<ResponsibleEmployee> Sort(ObservableCollection<ResponsibleEmployee> data)
        {
            return new ObservableCollection<ResponsibleEmployee>(data.OrderByDescending(x => x.TotalCount)
                                                                     .ThenByDescending(x => x.RkkCount));
        }
    }
}
