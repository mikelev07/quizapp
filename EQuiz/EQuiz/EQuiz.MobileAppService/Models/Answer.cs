using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQuiz.MobileAppService.Models
{

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPaswword { get; set; }
    }
    public class Answer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }

    public class Test
    {
        public int Id { get; set; }
        public ICollection<Question> Questions { get; set; }

        public Test()
        {
            Questions = new List<Question>();
        }

        public DateTime CreateDate { get; set; }

        public DateTime DateDeadline { get; set; }

    }

    public class UserTest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        public ICollection<UserAnswer> Answers { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DateDeadline { get; set; }
        public ICollection<User> Experts { get; set; }
        public UserTest()
        {
            Experts = new List<User>();
            Answers = new List<UserAnswer>();
        }
    }

    public class UserAnswer
    {
        public int Id { get; set; }
        public Answer Answer { get; set; }
        public int AnswerId { get; set; }
        public int? Raiting { get; set; }
      
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
