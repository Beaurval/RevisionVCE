using RevisionVCE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.Application.Interfaces
{
    public interface IPdfParser
    {
        IEnumerable<Question> GetAllQuestions();
        /// <summary>
        /// Converts the pdf into a string.
        /// </summary>
        /// <param name="pdfPath">Path to the pdf file.</param>
        /// <returns>The converted string.</returns>
        string PdfToText(string pdfPath);
        /// <summary>
        /// Split the text in a array of string. This array contains each questions of the pdf file.
        /// </summary>
        /// <param name="pdfText">Pdf text.</param>
        /// <returns>The text splited.</returns>
        string[] SplitTextByQuestions(string pdfText);
        void ScrapQuestionChoices(string questionText, Question question);
        void ScrapQuestionAnswers(string questionText, Question question);
        void ScrapQuestionTopic(string questionText, Question question);
        void ScrapQuestionText(string questionText, Question question);
        void ScrapQuestionExplanation(string questionText, Question question);
    }
}
