using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace TestSitek.Common
{
    internal class FileReadWrite
    {
        internal static async Task<string> ReadToEndAsync(string source, string text)
        {
            try
            {
                using (StreamReader file = new StreamReader(source))
                {
                    return await file.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения\n{ex.Message}");
                return string.Empty;
            }
        }

        internal static async Task SaveFileAsync(string source, ObservableCollection<ResponsibleEmployee> data, DateTimeOffset timeStartProgtam,string sortName)
        {
            try
            {
                string ReportName = $"{source}\\Отчет {DateTime.Now.ToString("d")}.txt";
                string ReportFullname;

                using (FileStream myReport = File.Create(ReportName))
                {
                    ReportFullname = myReport.Name;
                }

                using (StreamWriter file = new StreamWriter(ReportFullname))
                {
                    await file.WriteLineAsync($"Справка о неисполненных документах и обращениях граждан\n");

                    await file.WriteLineAsync($"Не исполнено в срок {data.Sum(x => x.TotalCount)} документов, из них:");
                    await file.WriteLineAsync($"-количество неисполненых РКК: {data.Sum(x => x.RkkCount)}");
                    await file.WriteLineAsync($"-количество неисполненных обращений: {data.Sum(x => x.ApealCount)}\n");


                    await file.WriteLineAsync($"Сортировка: {sortName}\n");

                    await file.WriteLineAsync("N".PadRight(10) 
                            +"ФИО".PadRight(35)
                            + "РКК".PadRight(15)
                            + "Обращения".PadRight(15)
                            + "Общее кол-во".PadRight(15));

                    int index = 1;
                    foreach (var employee in data)
                        await file.WriteLineAsync($"{index++.ToString().PadRight(10)}{employee.FIO.PadRight(35)}" +
                            $"{employee.RkkCount.ToString().PadRight(15)}" +
                            $"{employee.ApealCount.ToString().PadRight(15)}" +
                            $"{employee.TotalCount.ToString().PadRight(15)}");

                    await file.WriteLineAsync($"\nДата и время создания отчета: {DateTimeOffset.Now}" +
                                            $"\nДата и время запуска программы: {timeStartProgtam}\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи\n{ex.Message}");
            }
        }






    }
}
