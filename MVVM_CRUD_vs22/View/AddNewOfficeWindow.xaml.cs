using CRUD_mvvm_.ViewModel;
using System.Windows;


namespace CRUD_mvvm_.View
{
    public partial class AddNewOfficeWindow : Window
    {
        public AddNewOfficeWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
