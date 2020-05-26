using System.Collections.Generic;

namespace JML.ApiModels
{
    public class SectionGroupModel
    {
        public string Section { get; set; }
        public List<LectureModel> Lections { get; set; }
    }
}
