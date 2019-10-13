using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EQuiz.MobileAppService.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        ICollection<UserAnswer> userAnswers { get; set; }

        ICollection<UserTest> userTests { get; set; }

        public User()
        {
            userTests = new List<UserTest>();
            userAnswers = new List<UserAnswer>();
        }

     

    }
}
