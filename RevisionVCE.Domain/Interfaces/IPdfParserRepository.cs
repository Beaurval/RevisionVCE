using RevisionVCE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.Domain.Interfaces
{
    public interface IPdfParserRepository
    {
        IEnumerable<Question> GetQuestionsFromPdf();
        void InsertQuiz();
    }
}
