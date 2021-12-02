using RevisionVCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RevisionVCE.Infra.Models
{
    public class Question
    {
        public Question()
        {

        }

        public Question(QuestionModel questionModel)
        {
            Text = questionModel.Text;
            Explanation = questionModel.Explanation;
            Section = questionModel.Section;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Explanation { get; set; }
        public string Section { get; set; }
        

        //Relations
        public virtual Questionnaire Questionnaire { get; set; }
        public virtual ICollection<Choice> Choices { get; set; }

    }
}
