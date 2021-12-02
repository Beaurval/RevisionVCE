using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionVCE.Infra.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public char Letter { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
