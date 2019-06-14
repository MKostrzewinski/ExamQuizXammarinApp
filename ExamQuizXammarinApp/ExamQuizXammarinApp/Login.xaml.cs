using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamQuizXammarinApp.Database;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamQuizXammarinApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked_CreateAccount(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccount());
        }

        private async void Button_Clicked_AdminPanelLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminPanelLogin());
        }

        private async void Button_Clicked_Login(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(entryLogin.Text) || String.IsNullOrWhiteSpace(entryPassword.Text))
            {
                await DisplayAlert("Fail!", "You left empty fields", "OK");
                await Navigation.PushAsync(new Login());
            }
            else
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                var result = await firebaseHelper.FindUserByLoginAndPassword(entryLogin.Text, entryPassword.Text);
                if (result != null)
                {
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    await DisplayAlert("Fail!", "Wrong login or password", "OK");
                    await Navigation.PushAsync(new Login());
                }
            }

        }
    }
}