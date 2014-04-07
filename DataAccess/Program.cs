using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using System.Configuration;

    using Persons.DataAccessors;
    using Persons.Domain;

    class Program
    {

        /// <summary>
        /// Напечатать всех.
        /// </summary>
        /// <param name="accessor">Провайдер доступа к данным.</param>
        private static void Print(IDataAccessor<ContactInfo> accessor)
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

            //IDataAccessor accessor = new FileDataAccessor("data", persons);
            //IDataAccessor accessor = new ListDataAccessor(persons);
            IDataAccessor<ContactInfo> accessor = new MyORM<ContactInfo>();
            var people = accessor.GetAll();
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }

            Console.WriteLine();
            Console.Write("Введите Id для поиска:");
            int id = int.Parse(Console.ReadLine());
            var pers = accessor.GetById(id);
           
            Console.WriteLine(pers.ToString());
            
            Console.WriteLine();
            
            accessor.DeleteById(id);
            people = accessor.GetAll();
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine();
           
            Console.Write("Введите фамилию:");
            var address = Console.ReadLine();
            Console.Write("Введите имя:");
            var phone = Console.ReadLine();
            Console.Write("Введите отчество:");
            var email = Console.ReadLine();
            Console.Write("Введите возраст:");
            var age = int.Parse(Console.ReadLine());
            accessor.Insert(new ContactInfo(){Address = address, Phone = phone, Email = email});
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
            Console.WriteLine(accessor.GetById("Дубина").ToString());

            accessor.DeleteById("Зверева");
            Console.WriteLine("После удаления Зверевой: ");
            Print(accessor);*/
            Console.ReadKey();
        }
    }
}
