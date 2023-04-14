using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace TestSitek.Common
{
    internal static class AddEmployeesSimple
    {
        internal static void AddEmployees(string rkkText, string appealText, ObservableCollection<ResponsibleEmployee> data)
        {
            if (string.IsNullOrEmpty(rkkText) ||
                string.IsNullOrEmpty(appealText))
                return;

            try
            {
                AddRkk(rkkText, data);
                AddAppeal(appealText, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private static void AddRkk(string rkkText, ObservableCollection<ResponsibleEmployee> data)
        {
            foreach (var tempString in rkkText.Split(new char[] { '\n' }))
            {
                if (string.IsNullOrEmpty(tempString))
                    break;

                AddFioOrIncrementRkk(ReturnResponsible(tempString), data);
            }
        }

        private static void AddAppeal(string appealText, ObservableCollection<ResponsibleEmployee> data)
        {
            foreach (var tempString in appealText.Split(new char[] { '\n' }))
            {
                if (string.IsNullOrEmpty(tempString))
                    break;

                AddFioOrIncrementAppeal(ReturnResponsible(tempString), data);
            }
        }

        private static string ReturnResponsible(string line)
        {
            var tempSplit = line.Split(new char[] { '\t', ';' });
            string manager = tempSplit[0].Trim();

            foreach (var fio in tempSplit)
                if (fio.Contains("(Отв.)"))
                    return fio.Replace("(Отв.)", "").Trim();

            return FormatNameToShortView(manager);
        }

        private static string FormatNameToShortView(string fullName)
        {
            var fullNameSplit=fullName.Split(new char[] { ' ' });
            string result= fullNameSplit[0]+" "+fullNameSplit[1][0]+"."+fullNameSplit[2][0]+".";
            return result;
        
        }

        private static void AddFioOrIncrementRkk(string fio, ObservableCollection<ResponsibleEmployee> data)
        {
            foreach (var x in data)
                if (x.FIO == fio)
                {
                    x.RkkCount++;
                    x.TotalCount++;
                    return;
                }
            data.Add(new ResponsibleEmployee(fio, 1, 0, 1));
        }
        private static void AddFioOrIncrementAppeal(string fio, ObservableCollection<ResponsibleEmployee> data)
        {
            foreach (var x in data)
                if (x.FIO == fio)
                {
                    x.ApealCount++;
                    x.TotalCount++;
                    return;
                }
            data.Add(new ResponsibleEmployee(fio, 0, 1, 1));
        }
    }
}
