using System;
using System.Collections.Generic;
using System.Text;

namespace EQuiz.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuestionName { get; set; }

    }

    public class UserAnswer
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
    }


    public class UserTest
    {
        public int Id { get; set; }
        public ICollection<UserAnswer> Answers {get; set;}

        public string UserId { get; set; }
        public UserTest()
        {
            Answers = new List<UserAnswer>();
        }

    }
}
