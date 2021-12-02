using RevisionVCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionVCE.Infra.Models
{
    public class Questionnaire
    {
        public Questionnaire() { }

        public Questionnaire(QuestionnaireModel questionnaireModel)
        {
            this.Titre = questionnaireModel.Titre;
            this.Questions = questionnaireModel.QuestionsModel.Select(qm => new Question(qm)).ToList();
        }

        public int Id { get; set; }
        public string Titre { get; set; }
        
        //Relations
        public virtual ICollection<Question> Questions { get; set; }
    }
}
