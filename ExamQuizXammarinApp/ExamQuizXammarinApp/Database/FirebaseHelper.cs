using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using ExamQuizXammarinApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;


namespace ExamQuizXammarinApp.Database
{
    class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-4df89.firebaseio.com/");

        /////////////////////////////////////////////////     Users    /////////////////////////////////////////////////

        public async Task<List<User>> GetAllUsers()
        {
            return (await firebase
              .Child("Users")
              .OnceAsync<User>()).Select(item => new User
              {
                  ID = item.Object.ID,
                  Username = item.Object.Username,
                  Password = item.Object.Password,
                  Email = item.Object.Email,
                  Score = item.Object.Score,
                  Totalscore = item.Object.Totalscore

              }).ToList();
        }


        public async Task AddUser(string username, string password, string email)
        {
            var result = await FindLastUserID();        //  =======>
            int id;
            if (result != null)
            {
                id = result.ID;
                id++;                        // this code block is responsible for ID autoincrement
            }
            else
            {
                id = 1;
            }                       //                      <========

            await firebase
              .Child("Users")
              .PostAsync(new User() { ID = id, Username = username, Password = password, Email = email, Score = 0, Totalscore = 0 });
        }


        public async Task<User> GetUser(int user_id)
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.ID == user_id).FirstOrDefault();
        }


        public async Task UpdateUser(int userId, string username, string password, string email)
        {
            var toUpdateUser = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.ID == userId).FirstOrDefault();

            await firebase
              .Child("Users")
              .Child(toUpdateUser.Key)
              .PutAsync(new User() { ID = userId, Username = username, Password = password, Email = email });
        }


        public async Task DeleteUser(int userId)
        {
            var toDeleteUser = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.ID == userId).FirstOrDefault();
            await firebase.Child("Users").Child(toDeleteUser.Key).DeleteAsync();

        }

        public async Task<User> FindUserByLogin(string login) // This function checks if the username is available
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.Username == login).FirstOrDefault();
        }



        public async Task<User> FindUserByLoginAndPassword(string login, string password) // This function is checking the login data
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.Username == login && a.Password == password).FirstOrDefault();
        }


        private async Task<User> FindLastUserID()  // this function is returning the highest ID in User table
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.OrderBy(User => User.ID).LastOrDefault();
        }


        public async Task<List<User>> GetAllUsersOrderedByScore()
        {
            return (await firebase
              .Child("Users")
              .OnceAsync<User>()).Select(item => new User
              {
                  Username = item.Object.Username,
                  Score = item.Object.Score
              }).OrderByDescending(User => User.Score).ToList();
        }

        /////////////////////////////////////////////////         Test Users Reset            /////////////////////////////////////////////

        public async Task DeleteTestUserForReset(string testUserLogin)
        {
            var toDeleteQuestion = (await firebase
              .Child("Users")
              .OnceAsync<User>()).Where(a => a.Object.Username == testUserLogin).FirstOrDefault();
            await firebase.Child("Users").Child(toDeleteQuestion.Key).DeleteAsync();

        }

        public async Task AddTestUserForReset(string username, string password, string email, int score, int totalScore)
        {
            var result = await FindLastUserID();        //  =======>
            int id;
            if (result != null)
            {
                id = result.ID;
                id++;                        // this code block is responsible for ID autoincrement
            }
            else
            {
                id = 1;
            }                       //                      <========

            await firebase
              .Child("Users")
              .PostAsync(new User() { ID = id, Username = username, Password = password, Email = email, Score = score, Totalscore = totalScore });
        }

        /////////////////////////////////////////////////         Admins                /////////////////////////////////////////////
        public async Task<List<Admins>> GetAllAdmins()
        {
            return (await firebase
              .Child("Admins")
              .OnceAsync<Admins>()).Select(item => new Admins
              {
                  ID = item.Object.ID,
                  Username = item.Object.Username,
                  Password = item.Object.Password,
                  Email = item.Object.Email,

              }).ToList();
        }
        public async Task<Admins> FindAdminsByLogin(string login) // This function checks if the admin name is available
        {
            var allAdmins = await GetAllAdmins();
            await firebase
              .Child("Admins")
              .OnceAsync<Admins>();
            return allAdmins.Where(a => a.Username == login).FirstOrDefault();
        }

        public async Task<Admins> FindAdminByLoginAndPassword(string login, string password) // This function is checking the login to admin panel data
        {
            var allAdmins = await GetAllAdmins();
            await firebase
              .Child("Admins")
              .OnceAsync<Admins>();
            return allAdmins.Where(a => a.Username == login && a.Password == password).FirstOrDefault();
        }

        private async Task<Admins> FindLastAdminsID()  // this function is returning the highest ID in Admins table
        {
            var allAdmins = await GetAllAdmins();
            await firebase
              .Child("Admins")
              .OnceAsync<Admins>();
            return allAdmins.OrderBy(Admins => Admins.ID).LastOrDefault();
        }

        public async Task AddAdmin(string username, string password, string email)
        {
            var result = await FindLastAdminsID();        //  =======>
            int id;
            if (result != null)
            {
                id = result.ID;
                id++;                        // this code block is responsible for ID autoincrement
            }
            else
            {
                id = 1;
            }                       //                      <========

            await firebase
              .Child("Admins")
              .PostAsync(new Admins() { ID = id, Username = username, Password = password, Email = email });
        }

        /////////////////////////////////////////////////     Suggested Questions    /////////////////////////////////////////////////

        public async Task<List<SuggestedQuestions>> GetAllSuggestedQuestions()  //This function returns all of the records in the form of a list
        {
            return (await firebase
              .Child("SuggestedQuestion")
              .OnceAsync<SuggestedQuestions>()).Select(item => new SuggestedQuestions
              {
                  ID = item.Object.ID,
                  Category = item.Object.Category,
                  QuestionText = item.Object.QuestionText,
                  CorrectAnswer = item.Object.CorrectAnswer,
                  WrongAnswer1 = item.Object.WrongAnswer1,
                  WrongAnswer2 = item.Object.WrongAnswer2,
                  WrongAnswer3 = item.Object.WrongAnswer3

              }).ToList();
        }

        public async Task<SuggestedQuestions> GetQuestion(int question_id)
        {
            var allQuestions = await GetAllSuggestedQuestions();
            await firebase
              .Child("SuggestedQuestion")
              .OnceAsync<SuggestedQuestions>();
            return allQuestions.Where(a => a.ID == question_id).FirstOrDefault();
        }

        public async Task<SuggestedQuestions> GetQuestion()
        {
            var allQuestions = await GetAllSuggestedQuestions();
            await firebase
              .Child("SuggestedQuestion")
              .OnceAsync<SuggestedQuestions>();
            return allQuestions.FirstOrDefault();
        }
        public async Task<SuggestedQuestions> FindLastSuggestedQuestionsID()  // this function is returning the highest ID in SuggestedQuestions table
        {
            var allSuggestedQuestions = await GetAllSuggestedQuestions();
            await firebase
              .Child("SuggestedQuestion")
              .OnceAsync<SuggestedQuestions>();
            return allSuggestedQuestions.OrderBy(SuggestedQuestions => SuggestedQuestions.ID).LastOrDefault();
        }

        public async Task AddSuggestedQuestion(string category, string questionText, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
        {
            var result = await FindLastSuggestedQuestionsID();        //  =======>
            int id;
            if (result != null)
            {
                id = result.ID;
                id++;                        // this code block is responsible for ID autoincrement
            }
            else
            {
                id = 1;
            }                       //                      <========
            await firebase
              .Child("SuggestedQuestion")
              .PostAsync(new SuggestedQuestions() { ID = id, Category = category, QuestionText = questionText, CorrectAnswer = correctAnswer, WrongAnswer1 = wrongAnswer1, WrongAnswer2 = wrongAnswer2, WrongAnswer3 = wrongAnswer3 });
        }


        public async Task DeleteSuggestedQuestion(int questionId)
        {
            var toDeleteQuestion = (await firebase
              .Child("SuggestedQuestion")
              .OnceAsync<SuggestedQuestions>()).Where(a => a.Object.ID == questionId).FirstOrDefault();
            await firebase.Child("SuggestedQuestion").Child(toDeleteQuestion.Key).DeleteAsync();

        }

        ////////////////////////////////////////////////////   Questions ///////////////////////////////////////////////


        public async Task AddQuestion(string category, string questionText,
                                       string correctAnswer, string wrongAnswer1, string wrongAnswer2,
                                       string wrongAnswer3)
        {
            var result = await FindLastSuggestedQuestionsID();        //  =======>

            var id = await FindLastQuestionID();
            int id_value = id.ID;

            if (result != null)
            {


                id_value++;                        // this code block is responsible for ID autoincrement
            }
            else
            {
                id_value = 1;
            }                       //                      <========
            await firebase
              .Child("Question")
              .PostAsync(new Questions()
              {
                  ID = id_value,
                  Category = category,
                  QuestionText = questionText,
                  CorrectAnswer = correctAnswer,
                  WrongAnswer1 = wrongAnswer1,
                  WrongAnswer2 = wrongAnswer2,
                  WrongAnswer3 = wrongAnswer3
              });
        }

        public async Task<Questions> GetAllQuestionsByCategory(string category, int id)  // This function returns question from specify category based on ID
        {
            var allQuestions = await GetAllQuestions();
            await firebase
             .Child("Question")
             .OnceAsync<Questions>();
            return allQuestions.Where(a => a.Category == category && a.ID == id).FirstOrDefault();
        }

     


        public async Task<List<Questions>> GetAllQuestions()  //This function returns all of the records in the form of a list
        {
            return (await firebase
              .Child("Question")
              .OnceAsync<Questions>()).Select(item => new Questions
              {
                  ID = item.Object.ID,
                  Category = item.Object.Category,
                  QuestionText = item.Object.QuestionText,
                  CorrectAnswer = item.Object.CorrectAnswer,
                  WrongAnswer1 = item.Object.WrongAnswer1,
                  WrongAnswer2 = item.Object.WrongAnswer2,
                  WrongAnswer3 = item.Object.WrongAnswer3

              }).ToList();
        }

        public async Task<Questions> FindLastQuestionID(string category)  // this function is returning the highest ID in Questions table based on category
        {
            var allQuestions = await GetAllQuestions();
            await firebase
              .Child("Question")
              .OnceAsync<Questions>();
            return allQuestions.Where(a => a.Category == category).OrderBy(Questions => Questions.ID).LastOrDefault();
        }
        public async Task<Questions> FindLastQuestionID()  // this function is returning the highest ID in Questions table
        {
            var allQuestions = await GetAllQuestions();
            await firebase
              .Child("Question")
              .OnceAsync<Questions>();
            return allQuestions.OrderBy(Questions => Questions.ID).LastOrDefault();
        }


        public async Task<Questions> FindFirstQuestionID(string category)  // this function is returning the lowest ID in Questions table based on category
        {
            var allQuestions = await GetAllQuestions();
            await firebase
              .Child("Question")
              .OnceAsync<Questions>();
            return allQuestions.Where(a => a.Category == category).OrderBy(Questions => Questions.ID).FirstOrDefault();
        }
    }
}
