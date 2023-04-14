using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSitek.Common
{
    internal static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                // ignored
            }
        }
    }
}
