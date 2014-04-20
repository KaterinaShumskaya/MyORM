using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient
{
    using Persons.DataAccessors;
    using Persons.Domain;

    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private IDataAccessor<Person> _accessor;

        private Person _selectedItem;

        private static IList<Person> _persons = new List<Person>();

        public MainPage()
        {
            InitializeComponent();
            SetComboBoxData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var addItemPage = new AddItemPage(_accessor);
            this.NavigationService.Navigate(addItemPage);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void personsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = ((DataGrid)sender).SelectedItem as Person;
            this.deleteButton.IsEnabled = _selectedItem != null;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem == null)
            {

            }
            else
            {
                _accessor.DeleteById(_selectedItem.Id);
                this.RefreshData();
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value  = modeSelectionComboBox.SelectedItem as string;
            if (value == "Список")
            {
               _accessor = new ListDataAccessor<Person>(_persons);
            }
            else if (value == "Файл")
            {
                _accessor = new FileDataAccessor<Person>("persons.xml");
            }
            else if (value == "Ado.Net")
            {
                _accessor = new AdoNetDataAccessor();
            }
            else
            {
                _accessor = new MyORM<Person>();
            }
            this.RefreshData();
        }

        private void SetComboBoxData()
        {
            var list = new[] { "Список", "Файл", "Ado.Net", "MyORM" };
            modeSelectionComboBox.ItemsSource = list;
            modeSelectionComboBox.SelectedIndex = 0;
            _accessor = new ListDataAccessor<Person>(_persons);
        }

        private void RefreshData()
        {
            personsGrid.ItemsSource = _accessor.GetAll();
            CheckIsRepositoryEmpty();
        }

        private void CheckIsRepositoryEmpty()
        {
            if ((personsGrid.ItemsSource == null) || !personsGrid.ItemsSource.OfType<Person>().Any())
            {
                emptyRepositoryMessageLabel.Visibility = Visibility.Visible;
                personsGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                emptyRepositoryMessageLabel.Visibility = Visibility.Hidden;
                personsGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
