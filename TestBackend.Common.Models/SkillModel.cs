using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBackend.Common.Models
{
    public class SkillModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }

        public SkillModel() { }
        public SkillModel(string name, byte level)
        {
            Name = name;
            Level = level;
        }
    }
}
