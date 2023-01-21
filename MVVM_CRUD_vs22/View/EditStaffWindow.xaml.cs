using System.Text.RegularExpressions;
using System.Windows;
using CRUD_mvvm_.Model;
using CRUD_mvvm_.ViewModel;

namespace CRUD_mvvm_.View
{
    public partial class EditStaffWindow : Window
    {
        public EditStaffWindow(Staff staffToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedStaff = staffToEdit;
            DataManageVM.StaffName = staffToEdit.Name;
            DataManageVM.StaffSurName = staffToEdit.SurName;
            DataManageVM.StaffPhone = staffToEdit.Phone;
            //DataManageVM.StaffPosition = staffToEdit.Position;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
