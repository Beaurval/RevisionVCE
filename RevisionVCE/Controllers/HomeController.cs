﻿
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RevisionVCE.Domain.Models;
using RevisionVCE.Infra.Context;
using RevisionVCE.IServices;
using RevisionVCE.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RevisionVCE.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPdfParserService _pdfService;
        public HomeController(IPdfParserService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<IActionResult> Index()
        {
            //var model = TransformPdfIntoQuestions("D:\\dev\\source\\repos\\RevisionVCE\\RevisionVCE\\wwwroot\\myFile.pdf");
            //Questionnaire questionnaire = new Questionnaire();

            //questionnaire.Titre = "VCE Questionnaire 116 questions";
            //questionnaire.Questions = model;

            //_context.Questionnaires.Add(questionnaire);
            //await _context.SaveChangesAsync();


            ////for (int i = 0; i < model.Count; i++)
            ////{
            ////    model[i].Questionnaire = questionnaire;
            ////}

            ////questionnaire.Questions = model;
            ////_context.Questions.AddRange(questionnaire.Questions);
            ////await _context.SaveChangesAsync();
            ///

            IEnumerable<QuestionModel> questions = _pdfService.GetQuestionsFromPdf();

            return View();
        }

        public List<Question> TransformPdfIntoQuestions(string pdfPath)
        {
            List<Question> questions = new List<Question>();

            PdfReader pdfReader = new PdfReader(pdfPath);
            PdfDocument pdfDoc = new PdfDocument(pdfReader);
            string allText = "";
            for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                allText += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
            }


            //Split the text by questions
            string[] textSplitedByQuestions = Regex.Split(allText, @"QUESTION [01]?[0-9][0-9]?");
            for (int i = 0; i < textSplitedByQuestions.Length; i++)
            {
                textSplitedByQuestions[i] = Regex.Replace(textSplitedByQuestions[i], @".*http.*|.*www.vceplus.*", "");
            }
            //Then process each questions
            foreach (string questionText in textSplitedByQuestions.Skip(1))
            {
                Question question = new Question(questionText);
                questions.Add(question);
            }

            return questions;
        }
    }
}
