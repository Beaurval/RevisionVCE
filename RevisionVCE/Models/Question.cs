using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RevisionVCE.Models
{
    public class Question
    {
        public Question(string textToProcess)
        {
            Choices = new Dictionary<char, string>();

            string[] textSplitedInLine = textToProcess.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            textSplitedInLine = textSplitedInLine.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
            //Récupération des réponses possibles
            Regex searchChoices = new Regex(@"\n[A-Z][.] .*");
            var results = searchChoices.Matches(textToProcess);

            foreach (Match match in results)
            {
                string choiceTxt = match.Value.Replace("\n", "");
                choiceTxt = choiceTxt.Replace("\r", "");

                string[] choiceSplited = choiceTxt.Split('.');
                Choices.Add(choiceSplited[0][0], choiceSplited[1].Trim());
            }

            //Récupération des réponses
            Regex searchAnswers = new Regex(@"Correct Answer: .*");
            results = searchAnswers.Matches(textToProcess);
            foreach (Match match in results)
            {
                string answerTxt = match.Value.Replace("\n", "");
                answerTxt = answerTxt.Trim().Replace("\r", "");

                if(answerTxt.Split("Correct Answer: ").Length > 1)
                    Answers = answerTxt.Split("Correct Answer: ")[1].ToCharArray();
            }

            //Récupération du texte de la question
            Regex searchChoice = new Regex(@"[A-Z][.] .*");

            int index = 0;
            bool isChoice = searchChoice.Match(textSplitedInLine[index]).Success;
            while (!isChoice && index < textSplitedInLine.Length - 1)
            {
                if (!String.IsNullOrEmpty(textSplitedInLine[index]))
                    Text += textSplitedInLine[index];
                index++;
                isChoice = searchChoice.Match(textSplitedInLine[index]).Success;
            }

            //Récupération de l'explication
            Regex searchExplanation = new Regex(@"Explanation\/Reference:\s\nExplanation:");
            Regex searcheReference = new Regex(@"Reference(s)?:");

            string[] explanation = searchExplanation.Split(textToProcess);

            if (explanation.Length > 1)
            {
                Explanation += searcheReference.Replace(explanation[1], "").Trim().Replace("\n","");
            }

            //Récupération de la section
            Regex searchSection = new Regex(@"Section: .*");
            results = searchSection.Matches(textToProcess);
            foreach (Match match in results)
            {
                string sectionText = match.Value.Replace("\n", "");
                sectionText = sectionText.Replace("\r", "");

                Section = sectionText.Split("Section: ")[1].Trim();
            }
        }

        public string Text { get; set; }
        public string Explanation { get; set; }
        public string Section { get; set; }

        public Dictionary<char, string> Choices { get; set; }
        public char[] Answers { get; set; }

    }
}
