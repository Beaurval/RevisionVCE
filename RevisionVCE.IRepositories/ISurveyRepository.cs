using RevisionVCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.IRepositories
{
    public interface ISurveyRepository
    {
        Task AddQuiz(QuestionnaireModel questionnaireModel);
    }
}
