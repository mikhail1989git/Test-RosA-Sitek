using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSitek.Common
{
    public static class ProcessExtensions
    {
        public static bool IsRunning(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
