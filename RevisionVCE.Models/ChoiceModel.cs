using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionVCE.Models
{
    public class ChoiceModel
    {
        public char Letter { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
