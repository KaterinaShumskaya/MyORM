using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using System.Configuration;

    class Program
    {

        /// <summary>
        /// Напечатать всех.
        /// </summary>
        /// <param name="accessor">Провайдер доступа к данным.</param>
        private static void Print(IPersonAccessor accessor)
        {
            foreach (var person in accessor.GetAll())
            {
                Console.WriteLine(person.ToString());
            }
        }

        static void Main(string[] args)
        {

           /* var persons = new List<Person>
                              {
                                  new Person("Иванов", "Иван", "Иванович", 20),
                                  new Person("Сергеев", "Иван", "Иванович", 22),
                                  new Person("Антонов", "Иван", "Петрович", 21),
                                  new Person("Свиридова", "Мария", "Ивановна", 18),
                                  new Person("Романов", "Игорь", "Иванович", 25),
                                  new Person("Борисов", "Иван", "Ильич", 27),
                                  new Person("Телегин", "Олег", "Иванович", 22),
                                  new Person("Юрьев", "Иван", "Иванович", 24),
                                  new Person("Сушкин", "Артём", "Иванович", 25),
                                  new Person("Ломакина", "Ирина", "Юрьевна", 24),
                                  new Person("Петрова", "Светлана", "Васильевна", 23),
                                  new Person("Зверева", "Анна", "Леонидовна", 26)
                              };*/

            //IPersonAccessor accessor = new FilePersonAccessor("data", persons);
            //IPersonAccessor accessor = new ListPersonAccessor(persons);
            IPersonAccessor accessor = new MyORM();
            var people = accessor.GetAll();
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine();
            Console.Write("Введите фамилию для поиска:");
            string lastName = Console.ReadLine();
            people = accessor.GetByLastName(lastName);
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine();
            accessor = new ADONetPersonAccessor();
            accessor.DeleteByLastName(lastName);
            people = accessor.GetAll();
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine();
           
            Console.Write("Введите фамилию:");
            lastName = Console.ReadLine();
            Console.Write("Введите имя:");
            var firstName = Console.ReadLine();
            Console.Write("Введите отчество:");
            var middleName = Console.ReadLine();
            Console.Write("Введите возраст:");
            var age = int.Parse(Console.ReadLine());
            accessor.Insert(new Person(lastName, firstName, middleName, age));
            people = accessor.GetAll();
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }
           /* Console.WriteLine("Первоначальное состояние: ");
            Print(accessor);

            var newPerson = new Person("Дубина", "Сергей", "Митрофанович", 40);
            accessor.Insert(newPerson);
            Console.WriteLine("Добавленный элемент: ");
            Console.WriteLine(accessor.GetByLastName("Дубина").ToString());

            accessor.DeleteByLastName("Зверева");
            Console.WriteLine("После удаления Зверевой: ");
            Print(accessor);*/
            Console.ReadKey();
        }
    }
}
