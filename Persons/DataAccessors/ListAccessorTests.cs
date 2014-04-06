using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    class ListAccessorTests
    {
        private IList<Person> _persons;

        private IDataAccessor<Person> _accessor;
        
        /// <summary>
        /// Настройки.
        /// </summary>
        [SetUp]
        public void TestSetUp()
        {
            _persons = new List<Person>();
            _persons.Add(new Person("Иванов", "Иван", "Иванович", 20));
            _persons.Add(new Person("Сергеев", "Иван", "Иванович", 30));
            _accessor = new ListDataAccessor<Person>(_persons);
        }

        /// <summary>
        /// Тест получения списка людей.
        /// </summary>
        [Test]
        public void GetAllShouldPassSuccessfully()
        {
            IList<Person> persons = _accessor.GetAll();
            persons.Count.Should().Be(2);
            persons.ShouldBeEquivalentTo(new List<Person>
                                              {
                                                  new Person("Иванов", "Иван", "Иванович", 20),
                                                  new Person("Сергеев", "Иван", "Иванович", 30)
                                              });
        } 

        /// <summary>
        /// Тест добавления человека в список.
        /// </summary>
        [Test]
        public void InsertShouldPassSuccessfully()
        {
            var person = new Person("Антонов", "Иван", "Петрович", 20);
            _accessor.Insert(person);
            _persons.Count.Should().Be(3);
            _persons.SingleOrDefault(x => x.LastName.Equals("Антонов")).Should().NotBeNull();
        }

        /// <summary>
        /// Тест получения человека из списка по фамилии.
        /// </summary>
        [Test]
        public void GetByLastNameShouldPassSuccessfully()
        {
            Person person = _accessor.GetById(1);
            person.Should().NotBeNull();
            person.ShouldBeEquivalentTo(
                new Person("Иванов", "Иван", "Иванович", 20));
        }

        /// <summary>
        /// Тест удаления человека из списка по фамилии.
        /// </summary>
        [Test]
        public void DeleteByLastNameShouldPassSuccessfully()
        {
            _accessor.DeleteById(1);
            IList<Person> persons = _accessor.GetAll();
            persons.Count.Should().Be(1);
            persons.SingleOrDefault(x => x.LastName.Equals("Иванов")).Should().BeNull();
        }
    }
}
