using System.Text.RegularExpressions;
using System.Windows;
using CRUD_mvvm_.Model;
using CRUD_mvvm_.ViewModel;

namespace CRUD_mvvm_.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewPositionWindow.xaml
    /// </summary>
    public partial class EditPositionWindow : Window
    {
        public EditPositionWindow(Position positionToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedPosition = positionToEdit;
            DataManageVM.PositionName = positionToEdit.Name;
            DataManageVM.PositionSalary = positionToEdit.Salary;
            DataManageVM.PositionMaxNumber = positionToEdit.MaxStaff;
        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
