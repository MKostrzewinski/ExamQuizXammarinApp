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
            var res = await DisplayAlert("Are you sure?", "This action generates a lot of data traffic and may take some time", "Ok", "Cancel");
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
    }
}