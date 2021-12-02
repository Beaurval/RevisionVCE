using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.Models
{
    public class QuestionnaireModel
    {
        public int Id { get; set; }
        public string Titre { get; set; }

        public ICollection<QuestionModel> QuestionsModel { get; set; }
    }
}
