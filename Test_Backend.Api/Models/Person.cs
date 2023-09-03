using System.Numerics;
using TestBackend.Common.Models;

namespace TestBackend.Api.Models
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<Skill> Skills { get; set; }

        public Person() { }
        public Person(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
            Skills = new List<Skill>();
        }

        public PersonModel ToDto()
        {
            return new PersonModel()
            {
                Id = Id,
                Name = Name,
                DisplayName = DisplayName,
                Skills = Skills?.Select(s => s.ToDto()).ToList()
            };
        }
    }
}