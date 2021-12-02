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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task Add(ICollection<QuestionModel> questions)
        {
            await _questionRepository.Add(questions);
        }

        public Task Add(IEnumerable<QuestionModel> questions)
        {
            throw new NotImplementedException();
        }

        public async Task AddQuiz(QuestionnaireModel questionnaireModel)
        {
            await _questionRepository.AddQuiz(questionnaireModel);
        }
    }
}
