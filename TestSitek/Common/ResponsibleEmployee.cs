using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSitek.Common
{
    public class ResponsibleEmployee
    {
        private string fio;
        private int rkkCount;
        private int apealCount;
        private int totalCount;

        public string FIO { get => fio; set { fio = value; } }
        public int RkkCount { get => rkkCount; set { rkkCount = value; } }
        public int ApealCount { get => apealCount; set { apealCount = value; } }
        public int TotalCount { get => totalCount; set { totalCount = value; } }

        public ResponsibleEmployee()
        {
            fio = string.Empty;
            rkkCount = 0;
            apealCount = 0;
        }

        public ResponsibleEmployee(string fio, int rkkCount, int apealCount, int totalCount)
        {
            this.fio = fio;
            this.rkkCount = rkkCount;
            this.apealCount = apealCount;
            this.totalCount = totalCount;
        }

    }
}
