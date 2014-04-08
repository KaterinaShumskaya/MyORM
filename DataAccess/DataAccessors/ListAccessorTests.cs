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
            this._persons.Add(new Person(1, "Иванов", "Иван", "Иванович", 20));
            this._persons.Add(new Person(2, "Сергеев", "Иван", "Иванович", 30));
            this._accessor = new ListDataAccessor<Person>(this._persons);
        }

        /// <summary>
        /// Тест получения списка.
        /// </summary>
        [Test]
        public void GetAllShouldPassSuccessfully()
        {
            IList<Person> persons = this._accessor.GetAll();
            persons.Count.Should().Be(2);
            persons.ShouldBeEquivalentTo(new List<Person>
                                              {
                                                  new Person(1,"Иванов", "Иван", "Иванович", 20),
                                                  new Person(2,"Сергеев", "Иван", "Иванович", 30)
                                              });
        } 

        /// <summary>
        /// Тест добавления  в список.
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
        /// Тест получения из списка по идентификатору.
        /// </summary>
        [Test]
        public void GetByIdShouldPassSuccessfully()
        {
            Person person = this._accessor.GetById(1);
            person.Should().NotBeNull();
            person.ShouldBeEquivalentTo(
                new Person(1, "Иванов", "Иван", "Иванович", 20));
        }

        /// <summary>
        /// Тест удаления из списка по идентификатору.
        /// </summary>
        [Test]
        public void DeleteByIdShouldPassSuccessfully()
        {
            this._accessor.DeleteById(1);
            IList<Person> persons = this._accessor.GetAll();
            persons.Count.Should().Be(1);
            persons.SingleOrDefault(x => x.LastName.Equals("Иванов")).Should().BeNull();
        }
    }
}
