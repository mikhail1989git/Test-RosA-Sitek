using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using static System.Net.Mime.MediaTypeNames;

namespace TestSitek.Common
{
    internal class CommonMethods
    {
        internal static string OpenFileDialog()
        {
            Microsoft.Win32.OpenFileDialog ofDlg = new Microsoft.Win32.OpenFileDialog();

            ofDlg.Filter = "Text files (*.txt)|*.txt";
            ofDlg.CheckFileExists = true;

            if (ofDlg.ShowDialog() == true)
                return ofDlg.FileName;

            return string.Empty;
        }

        internal static string OpenFolderDialog()
        {
            FolderBrowserDialog ofDlg = new FolderBrowserDialog();

            if (ofDlg.ShowDialog()==DialogResult.OK)
                return ofDlg.SelectedPath;

            return string.Empty;
        }

        
    }
}
