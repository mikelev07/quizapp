using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQuiz.MobileAppService.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }


    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public Question()
        {
           Answers = new List<Answer>();
        }
    }
}
