using System.Collections.Generic;

namespace CollegeEnglish.DataModel
{
    public class Unit
    {
        public string UnitTitle { get; set; }
        public ICollection<Vocabulary> Vocabularies { get; set; }
    }
}