using TestBackend.Common.Models;

namespace TestBackend.Api.Models
{
    public class Skill
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }

        public Skill() { }
        public Skill(string name, byte level)
        {
            Name = name;
            Level = level;
        }

        public SkillModel ToDto()
        {
            return new SkillModel()
            {
                Id = Id,
                Name = Name,
                Level = Level
            };
        }
    }
}
