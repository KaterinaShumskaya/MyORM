namespace Persons.Domain
{
    using Persons.Attributes;

    public class EntityBase
    {
        [Identity("Id", "int")]
        public int Id { get; set; }
    }
}
