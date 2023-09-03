using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBackend.Common.Models
{
    public class PersonModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IList<SkillModel> Skills { get; set; }


        public PersonModel() { }
        public PersonModel(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }
    }


}
