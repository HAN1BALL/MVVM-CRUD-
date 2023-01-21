using System.Windows;
using CRUD_mvvm_.Model;
using CRUD_mvvm_.ViewModel;

namespace CRUD_mvvm_.View
{
    public partial class EditOfficeWindow : Window
    {
        public EditOfficeWindow(Office officeToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedOffice = officeToEdit;
            DataManageVM.OfficeName = officeToEdit.Name;
            DataManageVM.OfficeINN = officeToEdit.Inn;
            DataManageVM.OfficeAddress = officeToEdit.Address;
        }
    }
}
