using System.Windows;
using System.Windows.Controls;
using System.Data;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;

namespace Lab_2_NYSS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<UPI> last = new ObservableCollection<UPI>();
        Thread myThread;

        static int numberofElementforPage = 0;
        public MainWindow()
        {
            InitializeComponent();
            myThread = new Thread(UpdateInfo2);//каждую 1 минуту вызывается метод
            myThread.Start();
        }


        private void OpenFile_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dataTable;
                MainGrid.Items.Refresh();
                dataTable = Epplus_Parser.DatatableForGrid();
                MainGrid.ItemsSource = dataTable.DefaultView;
                MainGrid.Columns[0].Header = Epplus_Parser.columnNames[0];
                MainGrid.Columns[1].Header = Epplus_Parser.columnNames[1];
                MainGrid.Columns[2].Header = Epplus_Parser.columnNames[2];
                MainGrid.Columns[3].Header = Epplus_Parser.columnNames[3];
                MainGrid.Columns[4].Header = Epplus_Parser.columnNames[4];
                MainGrid.Columns[5].Header = Epplus_Parser.columnNames[5];
                MainGrid.Columns[6].Header = Epplus_Parser.columnNames[6];
                MainGrid.Columns[7].Header = Epplus_Parser.columnNames[7];
                foreach (DataGridColumn column in MainGrid.Columns)
                {
                    column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!!! Повторите действие!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            myThread.Abort();
            Application.Current.Shutdown();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)//создание нового файла
        {
            try
            {
                if (UPI.UPIs.Count <= 0)
                {
                    MessageBox.Show("Файл еще не загружен!!!");
                }
                else
                {
                    if (ShortRadio.IsChecked == true)
                    {
                        MessageBox.Show("Сохраняйте записи в полном виде!");
                    }
                    else
                    {
                        DatagridToDatatable();//переводим данные из datagrid в datatable. Далее из datatable в нашу коллекцию
                        ExportData.OutputData();
                        MessageBox.Show("Файл создан!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void DatagridToDatatable()//переводим данные из datagrid в datatable. Далее из datatable в нашу коллекцию
        {

            DataTable dt = new DataTable();
            dt = ((DataView)MainGrid.ItemsSource).ToTable();
            List<UPI> uPIs = new List<UPI>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UPI pI = new UPI();
                pI.id = dt.Rows[i][0].ToString();
                pI.name = dt.Rows[i][1].ToString();
                pI.description = dt.Rows[i][2].ToString();
                pI.danger = dt.Rows[i][3].ToString();
                pI.target = dt.Rows[i][4].ToString();
                pI.confidential = dt.Rows[i][5].ToString();
                pI.integriti = dt.Rows[i][6].ToString();
                pI.access = dt.Rows[i][7].ToString();
                uPIs.Add(pI);
            }

            List<UPI> rowsToDelete = new List<UPI>();
            for (int i = 0; i < uPIs.Count / 2; i++)
            {
                UPI upi = uPIs[i];
                rowsToDelete.Add(upi);
            }
            foreach (UPI upi in rowsToDelete)
            {
                uPIs.Remove(upi);
            }
            UPI.UPIs = uPIs;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {         
                UpdateInfo();     
        }
        private void UpdateInfo2()//для треды
        {
            TimeSpan interval = new TimeSpan(0, 1, 0);
            Thread.Sleep(interval);
            try
            {
                if (UPI.UPIs.Count <= 0)
                {
                    MessageBox.Show("Файл еще не загружен!!!");
                }
                else
                {
                    Updater.Parse();
                    Updater.Compare();
                    Updater.OutputResult();
                    for (int i = 0; i < Updater.stringsOut.Count; i++)
                    {
                        ListOfUpdated.Items.Add(Updater.stringsOut[i]);
                    }
                    MessageBox.Show("Обновлено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateInfo()//для кнопки
        {
            try
            {
                if (UPI.UPIs.Count <= 0)
                {
                    MessageBox.Show("Файл еще не загружен!!!");
                }
                else
                {
                    Updater.Parse();
                    Updater.Compare();
                    Updater.OutputResult();
                    for (int i = 0; i < Updater.stringsOut.Count; i++)
                    {
                        ListOfUpdated.Items.Add(Updater.stringsOut[i]);
                    }
                    MessageBox.Show("Обновлено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FoorwardBut_Click(object sender, RoutedEventArgs e)
        {
            if (PagesRadio.IsChecked == true)
            {
                numberofElementforPage++;
                if (numberofElementforPage < Pagination.PagiUPIs.Count)
                {
                    DataTable data = Pagination.ForwardOrPrevDatatable(numberofElementforPage);
                    MainGrid.ItemsSource = data.DefaultView;
                    MainGrid.Columns[0].Header = Epplus_Parser.columnNames[0];
                    MainGrid.Columns[1].Header = Epplus_Parser.columnNames[1];
                    MainGrid.Columns[2].Header = Epplus_Parser.columnNames[2];
                    MainGrid.Columns[3].Header = Epplus_Parser.columnNames[3];
                    MainGrid.Columns[4].Header = Epplus_Parser.columnNames[4];
                    MainGrid.Columns[5].Header = Epplus_Parser.columnNames[5];
                    MainGrid.Columns[6].Header = Epplus_Parser.columnNames[6];
                    MainGrid.Columns[7].Header = Epplus_Parser.columnNames[7];
                    foreach (DataGridColumn column in MainGrid.Columns)
                    {
                        column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
                    }
                }
                else
                {
                    MessageBox.Show("Страницы закончились!!!");
                }
            }
            else
            {
                MessageBox.Show("Выберите пострничный вывод!!!");
            }

        }

        private void BackBut_Click(object sender, RoutedEventArgs e)
        {
            if (PagesRadio.IsChecked == true)
            {
                numberofElementforPage--;
                if (numberofElementforPage >= 0)
                {
                    DataTable data = Pagination.ForwardOrPrevDatatable(numberofElementforPage);
                    MainGrid.ItemsSource = data.DefaultView;
                    MainGrid.Columns[0].Header = Epplus_Parser.columnNames[0];
                    MainGrid.Columns[1].Header = Epplus_Parser.columnNames[1];
                    MainGrid.Columns[2].Header = Epplus_Parser.columnNames[2];
                    MainGrid.Columns[3].Header = Epplus_Parser.columnNames[3];
                    MainGrid.Columns[4].Header = Epplus_Parser.columnNames[4];
                    MainGrid.Columns[5].Header = Epplus_Parser.columnNames[5];
                    MainGrid.Columns[6].Header = Epplus_Parser.columnNames[6];
                    MainGrid.Columns[7].Header = Epplus_Parser.columnNames[7];
                    foreach (DataGridColumn column in MainGrid.Columns)
                    {
                        column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
                    }
                }
                else
                {
                    numberofElementforPage = 0;
                    MessageBox.Show("Это первая страница!!!");
                }
            }
            else
            {
                MessageBox.Show("Выберите пострничный вывод!!!");
            }
        }

        private void PagesRadio_Checked(object sender, RoutedEventArgs e)
        {
            numberofElementforPage = 0;
            DataTable data = Pagination.ForwardOrPrevDatatable(numberofElementforPage);
            MainGrid.ItemsSource = data.DefaultView;
            MainGrid.Columns[0].Header = Epplus_Parser.columnNames[0];
            MainGrid.Columns[1].Header = Epplus_Parser.columnNames[1];
            MainGrid.Columns[2].Header = Epplus_Parser.columnNames[2];
            MainGrid.Columns[3].Header = Epplus_Parser.columnNames[3];
            MainGrid.Columns[4].Header = Epplus_Parser.columnNames[4];
            MainGrid.Columns[5].Header = Epplus_Parser.columnNames[5];
            MainGrid.Columns[6].Header = Epplus_Parser.columnNames[6];
            MainGrid.Columns[7].Header = Epplus_Parser.columnNames[7];
            foreach (DataGridColumn column in MainGrid.Columns)
            {
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            }
        }

        private void ShortRadio_Checked(object sender, RoutedEventArgs e)
        {
            DataTable data = ShortandFullData.ShortDatatable();//краткая таблица
            MainGrid.ItemsSource = data.DefaultView;
            MainGrid.Columns[0].Header = Epplus_Parser.columnNames[0];
            MainGrid.Columns[1].Header = Epplus_Parser.columnNames[1];
            foreach (DataGridColumn column in MainGrid.Columns)
            {
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            }
        }


        private void FullRadio_Checked(object sender, RoutedEventArgs e)
        {
            DataTable data = ShortandFullData.FullDatatable();//полная таблица
            MainGrid.ItemsSource = data.DefaultView;
            MainGrid.Columns[0].Header = Epplus_Parser.columnNames[0];
            MainGrid.Columns[1].Header = Epplus_Parser.columnNames[1];
            MainGrid.Columns[2].Header = Epplus_Parser.columnNames[2];
            MainGrid.Columns[3].Header = Epplus_Parser.columnNames[3];
            MainGrid.Columns[4].Header = Epplus_Parser.columnNames[4];
            MainGrid.Columns[5].Header = Epplus_Parser.columnNames[5];
            MainGrid.Columns[6].Header = Epplus_Parser.columnNames[6];
            MainGrid.Columns[7].Header = Epplus_Parser.columnNames[7];
            foreach (DataGridColumn column in MainGrid.Columns)
            {
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            }
        }

    }
}
