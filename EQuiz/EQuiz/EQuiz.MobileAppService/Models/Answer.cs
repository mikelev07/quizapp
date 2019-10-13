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

    public class UserTest
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<UserAnswer> Answers { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public UserTest()
        {
            Answers = new List<UserAnswer>();
        }
    }

    public class UserAnswer
    {
        public int Id { get; set; }
        
        public int AnswerId { get; set; }
      
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
