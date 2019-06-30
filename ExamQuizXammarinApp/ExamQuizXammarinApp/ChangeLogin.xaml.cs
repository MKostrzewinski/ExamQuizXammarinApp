using ExamQuizXammarinApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamQuizXammarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeLogin : ContentPage
    {
        public ChangeLogin()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Submit(object sender, EventArgs e)
        {
            if (txtCurrentUsername.Text == Login.login)     // Checks if user is trying to change his own account
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                var result = await firebaseHelper.FindUserByLogin(txtNewUsername.Text);

                if (result == null)     //  Checks for white space
                {
                    if (String.IsNullOrWhiteSpace(txtNewUsername.Text) || String.IsNullOrWhiteSpace(txtPassword.Text) || String.IsNullOrWhiteSpace(txtCurrentUsername.Text))
                    {
                        await DisplayAlert("Fail!", "You left empty fields", "OK");
                        await Navigation.PushAsync(new ChangeLogin());
                    }
                    else
                    {

                        var result2 = await firebaseHelper.FindUserByLoginAndPassword(txtCurrentUsername.Text, txtPassword.Text);

                        if (result2 != null)        // Cheks login data
                        {
                            ChangeLoginFunction();
                        }
                        else
                        {
                            await DisplayAlert("Fail!", "Wrong login or password", "OK");
                            Login.CurrentUserID = 0;                  //just for case...
                            await Navigation.PushAsync(new Login());
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Fail", "This username is not available", "OK");
                    txtNewUsername.Text = string.Empty;
                }
            }
            else
            {
                await DisplayAlert("Fail", "Invalid current username", "OK");
                txtCurrentUsername.Text = string.Empty;
            }
        }

        private async void ChangeLoginFunction()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            var result = await firebaseHelper.GetUser(Login.CurrentUserID);             // Downloading the current user's data

            string newUsername = txtNewUsername.Text;                       // Just for clarity

            await firebaseHelper.UpdateUserScore(Login.CurrentUserID, result.Score, result.Totalscore, newUsername, result.Password, result.Email);     // Change Data

            Login.login = newUsername;

            await DisplayAlert("You changed your login", newUsername, "OK");

            await Navigation.PushAsync(new Settings());
        }
    }      
}