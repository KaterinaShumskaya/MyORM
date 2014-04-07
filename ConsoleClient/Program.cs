using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    using Persons.DataAccessors;
    using Persons.Domain;

    class Program
    {
        private static int SelectMode()
        {
            Console.WriteLine("Введите цифру для выбора провайдера: 1-Список, 2-Файл, 3-ADO.Net, 4-CustomORM");
            Console.WriteLine("Для завершения работы нажмите Esc");
            var mode = 5;
            var modeArr = new[] {1, 2, 3, 4 };
            while (!modeArr.Contains(mode))
            {
                if (!int.TryParse(Console.ReadLine(), out mode) || !modeArr.Contains(mode))
                {
                    Console.WriteLine("Неверный ввод!");
                }
            }
            return mode;
        }

        private static int SelectOperation()
        {
            Console.WriteLine(@"Для выбора операции введите цифру
                              1-Вывести все данные, 
                              2-Выбрать по идентификатору,
                              3-Добавить данные, 
                              4-Удалить данные");
            Console.WriteLine("Для смены режима введите 5, для выхода нажмите Esc");
            var mode = 6;
            var modeArr = new[] { 1, 2, 3, 4, 5 };
            while (!modeArr.Contains(mode))
            {
                if (!int.TryParse(Console.ReadLine(), out mode) || !modeArr.Contains(mode))
                {
                    Console.WriteLine("Неверный ввод!");
                }
            }
            Console.WriteLine(mode);
            return mode;
        }

        private static IDataAccessor<Person> GetAccessor(int mode)
        {
            switch (mode)
            {
                case 1:
                    return new ListDataAccessor<Person>(new List<Person>());
                case 2:
                    return new FileDataAccessor<Person>("persons");
                case 3:
                    return new AdoNetDataAccessor();
                case 4:
                    return new MyORM<Person>();
                default:
                    return null;
            }
        } 

        private static void DoOperation(int operation, IDataAccessor<Person> accessor)
        {

            switch (operation)
            {
                case 1:
                    foreach (var person in accessor.GetAll())
                    {
                        Console.WriteLine(person.ToString());
                    }

                    break;
                case 2:
                    Console.WriteLine("Введите идентификатор: ");
                    int id;
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.WriteLine("Неверный ввод.");
                    }
                    accessor.GetById(id);
                    break;
                case 3:
                    Console.Write("Введите фамилию:");
                    var surname = Console.ReadLine();
                    Console.Write("Введите имя:");
                    var name = Console.ReadLine();
                    Console.Write("Введите отчество:");
                    var fatherName = Console.ReadLine();
                    Console.Write("Введите возраст:");
                    int age;
                    while (!int.TryParse(Console.ReadLine(), out age))
                    {
                        Console.WriteLine("Неверный ввод.");
                    }

                    while (age < 0)
                    {
                        Console.WriteLine("Возраст не может быть меньше 0.");
                    }

                    accessor.Insert(new Person(surname, name, fatherName, age));
                    break;
                case 4:
                    Console.WriteLine("Введите идентификатор: ");
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.WriteLine("Неверный ввод.");
                    }

                    accessor.DeleteById(id);
                    break;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Для начала работы нажмите Enter");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                var mode = SelectMode();
                var accessor = GetAccessor(mode);
                var operation = 6;
                while (operation != 5)
                {
                    operation = SelectOperation();
                    DoOperation(operation, accessor);
                }
            }
        }
    }
}
