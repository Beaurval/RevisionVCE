
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RevisionVCE.Infra.Context;
using RevisionVCE.Infra.Models;
using RevisionVCE.IServices;
using RevisionVCE.Models;
using RevisionVCE.UnitOfWork;
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
        private readonly ISurveyService _questionService;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(
            IPdfParserService pdfService,
            ISurveyService questionService, 
            IUnitOfWork unitOfWork)
        {
            _pdfService = pdfService;
            _questionService = questionService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            QuestionnaireModel questionnaire = new QuestionnaireModel();
            questionnaire.Titre = "Mon super quizz";
            questionnaire.QuestionsModel = _pdfService.GetQuestionsFromPdf();

            //await _questionService.AddQuiz(questionnaire);
            //await _unitOfWork.SaveChangesAsync();

            return View();
        }
    }
}
