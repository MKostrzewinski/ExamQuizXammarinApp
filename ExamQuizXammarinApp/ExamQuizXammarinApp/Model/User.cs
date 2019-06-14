using System;
using System.Collections.Generic;
using System.Text;

namespace ExamQuizXammarinApp.Model
{
    class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
        public int Totalscore { get; set; }
    }
}
