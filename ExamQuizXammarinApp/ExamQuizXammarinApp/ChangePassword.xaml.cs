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
	public partial class ChangePassword : ContentPage
	{
		public ChangePassword ()
		{
			InitializeComponent ();
		}
        private async void Button_Clicked_Submit(object sender, EventArgs e)
        {
            if (txtUsername.Text == Login.login)     // Checks if user is trying to change his own account
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                if (String.IsNullOrWhiteSpace(txtNewPassword.Text) || String.IsNullOrWhiteSpace(txtPassword.Text) || String.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    await DisplayAlert("Fail!", "You left empty fields", "OK");
                    await Navigation.PushAsync(new ChangeLogin());
                }
                else
                {

                    var result2 = await firebaseHelper.FindUserByLoginAndPassword(txtUsername.Text, txtPassword.Text);

                    if (result2 != null)        // Cheks login data
                    {
                        ChangePasswordFunction();
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
                await DisplayAlert("Fail", "Invalid current username", "OK");
                txtUsername.Text = string.Empty;
            }
        }

        private async void ChangePasswordFunction()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            var result = await firebaseHelper.GetUser(Login.CurrentUserID);             // Downloading the current user's data

            string newPassword = txtNewPassword.Text;                       // Just for clarity

            await firebaseHelper.UpdateUserScore(Login.CurrentUserID, result.Score, result.Totalscore, result.Username, newPassword, result.Email);     // Change Data

            await DisplayAlert("You changed your password", "Now you need to login again", "OK");

            await Navigation.PushAsync(new Login());
        }
    }
}