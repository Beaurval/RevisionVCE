﻿using RevisionVCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.IServices
{
    public interface IQuestionService
    {
        Task Add(IEnumerable<QuestionModel> questions);
        Task AddQuiz(QuestionnaireModel questionnaireModel);
    }
}
