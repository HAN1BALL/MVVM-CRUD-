using CRUD_mvvm_.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace CRUD_mvvm_.View
{
    public partial class AddNewStaffWindow : Window
    {
        public AddNewStaffWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
