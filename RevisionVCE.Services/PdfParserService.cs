using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using RevisionVCE.IServices;
using RevisionVCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RevisionVCE.Services
{
    public class PdfParserService : IPdfParserService
    {
        public PdfParserService(){}

        public IEnumerable<QuestionModel> GetQuestionsFromPdf()
        {
            string pdfPath = "D:\\dev\\source\\repos\\RevisionVCE\\RevisionVCE\\wwwroot\\myFile.pdf";
            List<QuestionModel> questions = new List<QuestionModel>();

            //Then process each questions
            string pdfText = PdfToText(pdfPath);
            string[] pdfTextSlitedByQuestion = SplitTextByQuestions(pdfText);

            foreach (string questionText in pdfTextSlitedByQuestion.Skip(1))
            {
                QuestionModel question = new QuestionModel();
                ScrapQuestionChoices(questionText, question);
                ScrapQuestionAnswers(questionText, question);
                ScrapQuestionText(questionText, question);
                ScrapQuestionTopic(questionText, question);
                ScrapQuestionExplanation(questionText, question);

                questions.Add(question);
            }

            return questions;
        }

        private string PdfToText(string pdfPath)
        {
            PdfReader pdfReader = new PdfReader(pdfPath);
            PdfDocument pdfDoc = new PdfDocument(pdfReader);
            string pdfText = "";
            for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                pdfText += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
            }

            return pdfText;
        }

        private string[] SplitTextByQuestions(string pdfText)
        {
            //Split the text by questions
            string[] textSplitedByQuestions = Regex.Split(pdfText, @"QUESTION [01]?[0-9][0-9]?");
            for (int i = 0; i < textSplitedByQuestions.Length; i++)
            {
                textSplitedByQuestions[i] = Regex.Replace(textSplitedByQuestions[i], @".*http.*|.*www.vceplus.*", "");
            }

            return textSplitedByQuestions;
        }

        private void ScrapQuestionChoices(string questionText, QuestionModel question)
        {
            //Récupération des réponses possibles
            Regex searchChoices = new Regex(@"\n[A-Z][.] .*");
            var results = searchChoices.Matches(questionText);

            foreach (Match match in results)
            {
                string choiceTxt = match.Value.Replace("\n", "");
                choiceTxt = choiceTxt.Replace("\r", "");

                string[] choiceSplited = choiceTxt.Split('.');
                question.ChoicesModel.Add(new ChoiceModel { Letter = choiceSplited[0][0], Text = choiceSplited[1].Trim() });
            }
        }

        private void ScrapQuestionAnswers(string questionText, QuestionModel question)
        {
            //Récupération des réponses
            Regex searchAnswers = new Regex(@"Correct Answer: .*");
            var results = searchAnswers.Matches(questionText);
            foreach (Match match in results)
            {
                string answerTxt = match.Value.Replace("\n", "");
                answerTxt = answerTxt.Trim().Replace("\r", "");

                if (answerTxt.Split("Correct Answer: ").Length > 1)
                {
                    foreach (char correctAnswer in answerTxt.Split("Correct Answer: ")[1].ToCharArray())
                    {
                        List<ChoiceModel> answersToModify = question.ChoicesModel.Where(c => c.Letter == correctAnswer).ToList();
                        for (int i = 0; i < answersToModify.Count(); i++)
                        {
                            answersToModify[i].IsCorrect = true;
                        }
                    }
                }
            }
        }

        private void ScrapQuestionTopic(string questionText, QuestionModel question)
        {
            //Récupération de la section
            Regex searchChoices = new Regex(@"\n[A-Z][.] .*");
            Regex searchSection = new Regex(@"Section: .*");

            var results = searchSection.Matches(questionText);
            foreach (Match match in results)
            {
                string sectionText = match.Value.Replace("\n", "");
                sectionText = sectionText.Replace("\r", "");

                question.Section = sectionText.Split("Section: ")[1].Trim();
            }
        }

        private void ScrapQuestionText(string questionText, QuestionModel question)
        {
            //Récupération du texte de la question
            string[] textSplitedInLine = questionText.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Regex searchChoice = new Regex(@"[A-Z][.] .*");

            int index = 0;
            bool isChoice = searchChoice.Match(textSplitedInLine[index]).Success;
            while (!isChoice && index < textSplitedInLine.Length - 1)
            {
                if (!String.IsNullOrEmpty(textSplitedInLine[index]))
                    question.Text += textSplitedInLine[index];
                index++;
                isChoice = searchChoice.Match(textSplitedInLine[index]).Success;
            }
        }

        private void ScrapQuestionExplanation(string questionText, QuestionModel question)
        {
            //Récupération de l'explication
            Regex searchExplanation = new Regex(@"Explanation\/Reference:\s\nExplanation:");
            Regex searcheReference = new Regex(@"Reference(s)?:");

            string[] explanation = searchExplanation.Split(questionText);

            if (explanation.Length > 1)
            {
                question.Explanation += searcheReference.Replace(explanation[1], "").Trim().Replace("\n", "");
            }
        }
    }
}
