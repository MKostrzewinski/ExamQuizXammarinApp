using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamQuizXammarinApp.Database;

namespace ExamQuizXammarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPanel : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public AdminPanel ()
        {
            InitializeComponent();
        }


        private async void Button_Clicked_Add_Admin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNewAdmin());
        }

        private async void Button_Clicked_Verify_Question(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VerifyQuestions());
        }

        private async void Button_Clicked_Reset_Test_Users(object sender, EventArgs e)
        {
            var res = await DisplayAlert("Are you sure?", "This action generates a lot of data traffic and may take some time", "Continue", "Cancel");
            if (res)
            {
                DoReset();
            }
        }

        private async void DoReset()
        {
            await DisplayAlert("Resetting is in progress", "It may take a few minutes", "Ok");
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            for (int i = 1; i < 101; i++)
            {
                string login = "TestUser" + i.ToString();
                var result = await firebaseHelper.FindUserByLogin(login);
                if (result != null)
                {
                    await firebaseHelper.DeleteTestUserForReset(login);
                }
            }

            for (int i = 1; i < 101; i++)
            {
                string login = "TestUser" + i.ToString();
                string password = "TestUser";
                string email = "TestUser";

                Random random = new Random();

                int score = random.Next(101);
                int totalScore = random.Next(score, 501);

                await firebaseHelper.AddTestUserForReset(login, password, email, score, totalScore);
            }
            await DisplayAlert("Succes!", "Test users have been reset", "OK");
        }

        private async void Button_Clicked_Reset_Score(object sender, EventArgs e)
        {
            var res2 = await DisplayAlert("Are you sure?", "The score should NOT be reset more often than once a week! By the way it generates very large data traffic", "Continue", "Cancel");
            if (res2)
            {
                DoScoreReset();
            }
        }

        private async void DoScoreReset()
        {
            await DisplayAlert("Score resetting is in progress", "It may take a few minutes", "Ok");
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            var result = await firebaseHelper.FindLastUserID();     //finding last User
            int lastUser = result.ID;
            lastUser++;
            int ZeroValue = 0;      // just for clarity
            

            for(int i = 1; i < lastUser; i++)       // Updating all users by Id, one by one.
            {
                var result2 = await firebaseHelper.GetUser(i);

                if(result2 != null)
                {
                    await firebaseHelper.UpdateUserScore(i, ZeroValue, result2.Totalscore, result2.Username, result2.Password, result2.Email);
                }
            }


            await DisplayAlert("Succes!", "Scoreboard have been reset", "OK");
        }

    }
}