using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCAbstraction
{
    using System.Collections;

    using Persons.DataAccessors;
    using Persons.Domain;

    class Program
    {
        static void Main(string[] args)
        {
            IContainer ioc = null;
            while (ioc == null)
            {
                Console.WriteLine("Выберите IoC контейнер: 1-MyIoC, 2-Castle Windsor, 3-Unity, 4-Autofac");
                int iocNumber;
                int.TryParse(Console.ReadLine(), out iocNumber);
                switch (iocNumber)
                {
                    case 1:
                        {
                            ioc = new MyIOCContainer();
                            break;
                        }
                    case 2:
                        {
                            ioc = new WindsorAdapter();
                            break;
                        }
                    case 3:
                        {
                            ioc = new UnityAdapter();
                            break;
                        }
                    case 4:
                        {
                            ioc = new AutofacAdapter();
                            break;
                        }
                }
                if (ioc == null)
                {
                    Console.WriteLine("Неверный ввод!!!");
                }
            }
            var logger = new NLogAdapter();
            logger.Trace(String.Format("Выбран IoC : {0}", ioc.GetType()));
            ioc.Register<IList<Person>>(new List<Person>());
            ioc.Register<IDataAccessor<Person>, ListDataAccessor<Person>>();
            var dataAccessor = ioc.Resolve<IDataAccessor<Person>>();
            dataAccessor.Insert(new Person(1, "Петров", "Петр", "Петрович", 20));
            dataAccessor.Insert(new Person(2, "Иванов", "Иван", "Иванович", 25));
            foreach (var person in dataAccessor.GetAll())
            {
                logger.Info(String.Format("В коллекцию добавлен {0}", person.ToString()));
                Console.WriteLine(person.ToString());
            }

            Console.ReadKey();
        }
    }
}
