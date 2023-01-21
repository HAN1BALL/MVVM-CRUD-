using CRUD_mvvm_.Model;
using CRUD_mvvm_.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRUD_mvvm_.ViewModel
{
    internal class DataManageVM : INotifyPropertyChanged
    {
        //все отделы
        private List<Office> allOffices = DataWorker.GetAllOffices();
        public List<Office> AllOffices
        {
            get 
            { 
                return allOffices; 
            }
            set
            {
                allOffices = value;
                NotifyPropertyChanged("AllOffices");
            }
        }

        //все позиции
        private List<Position> allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get 
            { 
                return allPositions; 
            }
            set
            {
                allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }
        }

        //все сотрудники
        private List<Staff> allStaff = DataWorker.GetAllStaff();
        public List<Staff> AllStaff
        {
            get
            {
                return allStaff;
            }
            set
            {
                allStaff = value;
                NotifyPropertyChanged("AllStaff");
            }
        }

        #region Properties

        //свойства для офиса
        public static string OfficeName { get; set; }
        public static int OfficeINN { get; set; }
        public static string OfficeAddress { get; set; }

        //свойства для позиции
        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public static Office PositionOffice { get; set; }

        //свойства для сотрудника 
        public static string StaffName { get; set; }
        public static string StaffSurName { get; set; }
        public static string StaffPhone { get; set; }
        public static string StaffAddress { get; set; }
        public static decimal StaffSalary { get; set; }
        public static Position StaffPosition { get; set; }
        public static Office StaffOffice { get; set; }

        //свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static Staff SelectedStaff { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Office SelectedOffice { get; set; }

        #endregion


        #region COMMANDS TO ADD

        private RelayCommand addNewOffice;
        public RelayCommand AddNewOffice
        {
            get
            {
                return addNewOffice ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (OfficeName == null || OfficeName.Replace(" ", "").Length  == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (OfficeINN == 0)
                    {
                        SetRedBlockControll(wnd, "InnBlock");
                    }
                    if (OfficeAddress == null || OfficeAddress.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "AddressBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateOffice(OfficeName, OfficeINN, OfficeAddress);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (PositionSalary == 0)
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                    }
                    if (PositionMaxNumber == 0)
                    {
                        SetRedBlockControll(wnd, "MaxNumberBlock");
                    }
                    if (PositionOffice == null)
                    {
                        MessageBox.Show("Укажите офис!");
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionOffice);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand addNewStaff;
        public RelayCommand AddNewStaff
        {
            get
            {
                return addNewStaff ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (StaffName == null || StaffName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (StaffSurName == null || StaffSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                    }
                    if (StaffPhone == null || StaffPhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "MaxNumberBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateStaff(StaffName, StaffSurName, StaffPhone, StaffPosition);
                        UpdateAllDataView();
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        #endregion

        private RelayCommand deleteitem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteitem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если сотрудник
                    if(SelectedTabItem.Name == "StaffTab" && SelectedStaff != null)
                    {
                        resultStr = DataWorker.DeleteStaff(SelectedStaff);
                        UpdateAllDataView();
                    }
                    //если позиция
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }
                    //если офис
                    if (SelectedTabItem.Name == "OfficeTab" && SelectedOffice != null)
                    {
                        resultStr = DataWorker.DeleteOffice(SelectedOffice);
                        UpdateAllDataView();
                    }
                    //обновление
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }
                );
            }
        }

        #region EDIT COMMAND
        private RelayCommand editStaff;
        public RelayCommand EditStaff
        {
            get
            {
                return editStaff ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string noPositionStr = "Не выбрана новая должность";
                    if (SelectedStaff != null)
                    {
                        if (StaffPosition != null)
                        {
                            resultStr = DataWorker.EditStaff(SelectedStaff, StaffName, StaffSurName, StaffPhone, SelectedPosition);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noPositionStr);
                    }
                    
                });
            }
        }

        private RelayCommand editPosition;
        public RelayCommand EditPosition
        {
            get
            {
                return editPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана позиция";
                    string noPositionStr = "Не выбран новый офис";
                    if (SelectedPosition != null)
                    {
                        if (StaffOffice != null)
                        {
                            resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionSalary, PositionMaxNumber, PositionOffice);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noPositionStr);
                    }

                });
            }
        }

        private RelayCommand editOffice;
        public RelayCommand EditOffice
        {
            get
            {
                return editOffice ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран офис";
                    if (SelectedOffice != null)
                    {
                        resultStr = DataWorker.EditOffice(SelectedOffice, OfficeName, OfficeINN);

                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }

                });
            }
        }

        #endregion

        #region COMMANDS TO OPEN WINDOWS
        private RelayCommand openAddNewOfficeWnd;
        public RelayCommand OpenAddNewOfficeWnd
        {
            get
            {
                return openAddNewOfficeWnd ?? new RelayCommand(obj =>
                    {
                        OpenAddOfficeWindowMethod();
                    });
            }
        }
        private RelayCommand openAddNewPositionWnd;
        public RelayCommand OpenAddNewPositionWnd
        {
            get
            {
                return openAddNewPositionWnd ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                });
            }
        }

        private RelayCommand openAddNewStaffWnd;
        public RelayCommand OpenAddNewStaffWnd
        {
            get
            {
                return openAddNewStaffWnd ?? new RelayCommand(obj =>
                {
                    OpenAddStaffWindowMethod();
                });
            }
        }

        private RelayCommand openEditItemWnd;
        public RelayCommand OpenEditItem
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если сотрудник
                    if (SelectedTabItem.Name == "StaffTab" && SelectedStaff != null)
                    {
                        OpenEditStaffWindowMethod(SelectedStaff);
                    }
                    //если позиция
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindowMethod(SelectedPosition);
                    }
                    //если офис
                    if (SelectedTabItem.Name == "OfficeTab" && SelectedOffice != null)
                    {
                        OpenEditOfficeWindowMethod(SelectedOffice);
                    }
                }
                );
            }
        }

        #endregion

        #region METHODS TO OPEN WINDOW
        //методы открытия окон 
        private void OpenAddOfficeWindowMethod()
        {
            AddNewOfficeWindow newOfficeWindow = new AddNewOfficeWindow();
            SetCenterPositionAndOpen(newOfficeWindow);
        }
        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpen(newPositionWindow);
        }
        private void OpenAddStaffWindowMethod()
        {
            AddNewStaffWindow newStaffWindow = new AddNewStaffWindow();
            SetCenterPositionAndOpen(newStaffWindow);
        }

        //окна редактирования
        private void OpenEditOfficeWindowMethod(Office office)
        {
            EditOfficeWindow editOfficeWindow = new EditOfficeWindow(office);
            SetCenterPositionAndOpen(editOfficeWindow);
        }
        private void OpenEditPositionWindowMethod(Position position)
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow(position);
            SetCenterPositionAndOpen(editPositionWindow);
        }
        private void OpenEditStaffWindowMethod(Staff staff)
        {
            EditStaffWindow editStaffWindow = new EditStaffWindow(staff);
            SetCenterPositionAndOpen(editStaffWindow);
        }
        #endregion

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red; 
        }

        #region UPDATE VIEWS

        private void SetNullValuesToProperties()
        {
            //для сотрудника
            StaffName = null;
            StaffSurName = null;
            StaffPhone = null;
            StaffAddress = null;
            StaffPosition = null;
            //для позиции
            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionOffice = null;
            //для офиса
            OfficeName = null;
        }

        private void UpdateAllDataView()
        {
            UpdateAllOfficesView();
            UpdateAllPositionsView();
            UpdateAllStaffView();
        }

        private void UpdateAllOfficesView()
        {
            AllOffices = DataWorker.GetAllOffices();
            MainWindow.AllOfficesView.ItemsSource = null;
            MainWindow.AllOfficesView.Items.Clear();
            MainWindow.AllOfficesView.ItemsSource = AllOffices;
            MainWindow.AllOfficesView.Items.Refresh();
        }

        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }

        private void UpdateAllStaffView()
        {
            AllStaff = DataWorker.GetAllStaff();
            MainWindow.AllStaffView.ItemsSource = null;
            MainWindow.AllStaffView.Items.Clear();
            MainWindow.AllStaffView.ItemsSource = AllStaff;
            MainWindow.AllStaffView.Items.Refresh();
        }

        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }       


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
