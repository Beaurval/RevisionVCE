﻿using RevisionVCE.Infra.Context;
using RevisionVCE.Infra.Models;
using RevisionVCE.IRepositories;
using RevisionVCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly VceQuizzContext _context;

        public QuestionRepository(VceQuizzContext context)
        {
            _context = context;
        }

        public async Task Add(ICollection<QuestionModel> questionsModel)
        {
            var questions = questionsModel.Select(qm => new Question(qm));
            await _context.Questions.AddRangeAsync(questions);
        }

        public async Task AddQuiz(QuestionnaireModel questionnaireModel)
        {
           await  _context.Questionnaires.AddAsync(new Questionnaire(questionnaireModel));
        }
    }
}
