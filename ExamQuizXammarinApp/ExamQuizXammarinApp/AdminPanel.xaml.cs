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

        private async void Button_Clicked_Add_Question(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VerifyQuestions());
        }

        private async void Button_Clicked_Verify_Question(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VerifyQuestions());
        }
    }
}