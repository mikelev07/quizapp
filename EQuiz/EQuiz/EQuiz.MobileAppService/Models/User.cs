using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQuiz.MobileAppService.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        ICollection<UserAnswer> userAnswers { get; set; }

        public User()
        {
            userAnswers = new List<UserAnswer>();
        }

    }
}
