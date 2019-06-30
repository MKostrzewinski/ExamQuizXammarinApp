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
    public partial class GameMenu : ContentPage
    {
        
        
        

        public GameMenu()
        {
            InitializeComponent();
        }
        private async void Button_Clicked_SuggestQuestion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewQuestion());

        }
        

        private async void Button_Clicked_New_Game(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChooseCategory());
        }

        private async void Button_Clicked_Settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }

        private async void Button_Clicked_Scoreboard(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Scoreboard());
        }

        private async void Button_Clicked_My_Score(object sender, EventArgs e)
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            var result = await firebaseHelper.GetUser(Login.CurrentUserID);
            string title = "Score of " + result.Username + ":";
            await DisplayAlert(title, result.Score.ToString(), "OK");
        }

    }
}