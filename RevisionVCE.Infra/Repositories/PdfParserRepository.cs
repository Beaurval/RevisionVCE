using RevisionVCE.Domain.Interfaces;
using RevisionVCE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.Infra.Repositories
{
    class PdfParserRepository : IPdfParserRepository
    {
        public PdfParserRepository()
        {
        }

        public IEnumerable<Question> GetQuestionsFromPdf()
        {
            throw new NotImplementedException();
        }

        public void InsertQuiz()
        {
            throw new NotImplementedException();
        }
    }
}
