using CRUD_mvvm_.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CRUD_mvvm_.View
{
    public partial class MainWindow : Window
    {
        public static ListView AllOfficesView;
        public static ListView AllPositionsView;
        public static ListView AllStaffView;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllOfficesView = ViewAllOffices;
            AllPositionsView = ViewAllPositions;
            AllStaffView = ViewAllStaff;
        }
    }
}
