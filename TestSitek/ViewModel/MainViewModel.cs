using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using TestSitek.Command;
using TestSitek.Common;
using static System.Net.Mime.MediaTypeNames;

namespace TestSitek.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string rkkSource;
        private string appealSource;
        private string log= string.Empty;
        private string saveSource;

        public string RkkSource { get => rkkSource; set => Set(ref rkkSource, value); }
        public string AppealSource { get => appealSource; set => Set(ref appealSource, value); }
        public string Log { get => log; set => Set(ref log, value); }
        public string SaveSource { get => saveSource; set => Set(ref saveSource, value); }


        private ICommand rkkSourceButton;
        private ICommand appealSourceButton;
        private ICommand openButton;
        private ICommand saveSourceButton;
        private ICommand saveButton;
        private ICommand sortButton;
        private ICommand clearLogButton;

        public ICommand RkkSourceButton => rkkSourceButton ?? (rkkSourceButton = new RelayCommand(RkkSourceMethod));
        public ICommand AppealSourceButton => appealSourceButton ?? (appealSourceButton = new RelayCommand(AppealSourceMethod));
        public ICommand OpenButton => openButton ?? (openButton = new AsyncRelayCommand(OpenMethod));
        public ICommand SaveSourceButton => saveSourceButton ?? (saveSourceButton = new RelayCommand(SaveSourceMethod));
        public ICommand SaveButton => saveButton ?? (saveButton = new AsyncRelayCommand(SaveMethod));
        public ICommand SortButton => sortButton ?? (sortButton = new RelayCommand(SortMethod));
        public ICommand ClearLogButton => clearLogButton ?? (clearLogButton = new RelayCommand(ClearLogMethod));

        private string RkkText;
        private string AppealText;

        private ObservableCollection<ResponsibleEmployee> data = new ObservableCollection<ResponsibleEmployee>();
        public ObservableCollection<ResponsibleEmployee> Data { get => data; set => Set(ref data, value); }

        private DateTimeOffset TimeStartProgram;

        private bool sortByFio;
        private bool sortByRkk;
        private bool sortByAppeal;
        private bool sortByTotalCount;

        public bool SortByFio { get => sortByFio; set => Set(ref sortByFio, value); }
        public bool SortByRkk { get => sortByRkk; set => Set(ref sortByRkk, value); }
        public bool SortByAppeal { get => sortByAppeal; set => Set(ref sortByAppeal, value); }
        public bool SortByTotalCount { get => sortByTotalCount; set => Set(ref sortByTotalCount, value); }

        private string sortName;

        private Stopwatch stopwatch = new Stopwatch();

        public MainViewModel()
        {
            TimeStartProgram = DateTimeOffset.Now;
            Log += $"Программа запущена {TimeStartProgram}";
            sortName = "без сортировки";
        }

        private void RkkSourceMethod()
        {
            RkkSource = CommonMethods.OpenFileDialog();
        }

        private void AppealSourceMethod()
        {
            AppealSource = CommonMethods.OpenFileDialog();
        }

        private async Task OpenMethod()
        {
            if (string.IsNullOrWhiteSpace(rkkSource) ||
                string.IsNullOrWhiteSpace(appealSource))
            {
                MessageBox.Show("Поля пустые или заполнены некорректно.");
                return;
            }

            stopwatch.Start();
            RkkText = await FileReadWrite.ReadToEndAsync(rkkSource, RkkText);
            AppealText = await FileReadWrite.ReadToEndAsync(appealSource, AppealText);

            stopwatch.Stop();
            Log += $"\nВремя чтения исходных файлов {stopwatch.Elapsed}";

            stopwatch.Restart();
            AddEmployeesSimple.AddEmployees(RkkText, AppealText, Data);
            stopwatch.Stop();
            Log += $"\nВремя составления таблицы {stopwatch.Elapsed}";
        }



        private void SaveSourceMethod()
        {
            SaveSource = CommonMethods.OpenFolderDialog();
        }

        private async Task SaveMethod()
        {
            stopwatch.Start();

            await FileReadWrite.SaveFileAsync(SaveSource, Data, TimeStartProgram, sortName);

            stopwatch.Stop();
            Log += $"\nВремя записи файла отчета {stopwatch.Elapsed}";
        }

        private void SortMethod()
        {
            if (!SortByFio &&
                !SortByRkk &&
                !SortByAppeal &&
                !SortByTotalCount)
            {
                MessageBox.Show("Выберите поле для сортировки");
                return;
            }

            if (Data.Count == 0)
            {
                MessageBox.Show("Нет данных для сортировки");
                return;
            }

            stopwatch.Start();

            if (SortByFio)
            {
                Data = new SortByFio().Sort(Data);
                sortName = "без сортировки";
            }

            if (SortByRkk)
            {
                Data = new SortByRkk().Sort(Data);
                sortName = "по РКК";
            }


            if (SortByAppeal)
            {
                Data = new SortByAppeal().Sort(Data);
                sortName = "по обращениям";
            }


            if (SortByTotalCount)
            {
                Data = new SortByTotalCount().Sort(Data);
                sortName = "по общему количеству документов";
            }


            stopwatch.Stop();
            Log += $"\nСортировка {stopwatch.Elapsed}";
        }

        private void ClearLogMethod()
        {
            Log = string.Empty;
        }

    }
}