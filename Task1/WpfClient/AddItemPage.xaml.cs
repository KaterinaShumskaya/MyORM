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
    /// Interaction logic for AddItemPage.xaml
    /// </summary>
    public partial class AddItemPage : Page
    {
        private readonly IDataAccessor<Person> _accessor;

        public AddItemPage()
        {
            InitializeComponent();
        }

        public AddItemPage(IDataAccessor<Person> accessor) : this()
        {
            _accessor = accessor;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int age;
            if (!int.TryParse(ageTextBox.Text, out age) || (age < 0))
            {
                MessageBox.Show("Значение возраста введено не корректно", "Ошибка", MessageBoxButton.OK);
            }
            else
            {
                _accessor.Insert(new Person(lastNameTextBox.Text, firstNameTextBox.Text, middleNameTextBox.Text, age));
                lastNameTextBox.Text = "";
                firstNameTextBox.Text = "";
                middleNameTextBox.Text = "";
                ageTextBox.Text = "";
                this.NavigationService.GoBack();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
