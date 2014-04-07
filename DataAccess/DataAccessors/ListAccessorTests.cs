namespace Persons.DataAccessors
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    using Persons.Domain;

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
            this._persons = new List<Person>();
            this._persons.Add(new Person("Иванов", "Иван", "Иванович", 20));
            this._persons.Add(new Person("Сергеев", "Иван", "Иванович", 30));
            this._accessor = new ListDataAccessor<Person>(this._persons);
        }

        /// <summary>
        /// Тест получения списка людей.
        /// </summary>
        [Test]
        public void GetAllShouldPassSuccessfully()
        {
            IList<Person> persons = this._accessor.GetAll();
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
            this._accessor.Insert(person);
            this._persons.Count.Should().Be(3);
            this._persons.SingleOrDefault(x => x.LastName.Equals("Антонов")).Should().NotBeNull();
        }

        /// <summary>
        /// Тест получения человека из списка по фамилии.
        /// </summary>
        [Test]
        public void GetByLastNameShouldPassSuccessfully()
        {
            Person person = this._accessor.GetById(1);
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
            this._accessor.DeleteById(1);
            IList<Person> persons = this._accessor.GetAll();
            persons.Count.Should().Be(1);
            persons.SingleOrDefault(x => x.LastName.Equals("Иванов")).Should().BeNull();
        }
    }
}
