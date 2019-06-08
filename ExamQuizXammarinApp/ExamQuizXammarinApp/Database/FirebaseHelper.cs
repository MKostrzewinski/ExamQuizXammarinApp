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

        public async Task<List<User>> GetAllUsers()
        {
            return (await firebase
              .Child("Users")
              .OnceAsync<User>()).Select(item => new User
              {
                  ID = item.Object.ID,
                  Username = item.Object.Username,
                  Password = item.Object.Password,
                  Email = item.Object.Email

              }).ToList();
        }
        public async Task AddUser(int id, string username, string password, string email)
        {

            await firebase
              .Child("Users")
              .PostAsync(new User() { ID = id, Username = username, Password = password, Email = email });
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

        public async Task<User> FindUserByLogin(string login)
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.Username == login).FirstOrDefault();
        }

        public async Task<User> FindUserByLoginAndPassword(string login, string password)
        {
            var allUsers = await GetAllUsers();
            await firebase
              .Child("Users")
              .OnceAsync<User>();
            return allUsers.Where(a => a.Username == login || a.Password == password).FirstOrDefault();
        }

    }
}
