using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.Models
{
    public class QuestionModel
    {
        public QuestionModel()
        {
            ChoicesModel = new List<ChoiceModel>();
        }
        public string Text { get; set; }
        public string Explanation { get; set; }
        public string Section { get; set; }

        public ICollection<ChoiceModel> ChoicesModel { get; set; }
    }
}
