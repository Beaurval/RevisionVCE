using RevisionVCE.IRepositories;
using RevisionVCE.IServices;
using RevisionVCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.Services
{
    public class QuestionService : ISurveyService
    {
        private readonly ISurveyRepository _questionRepository;

        public QuestionService(ISurveyRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task AddQuiz(QuestionnaireModel questionnaireModel)
        {
            await _questionRepository.AddQuiz(questionnaireModel);
        }
    }
}
