using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionVCE.Domain.Models
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        
        //Relations
        public virtual ICollection<Question> Questions { get; set; }
    }
}
