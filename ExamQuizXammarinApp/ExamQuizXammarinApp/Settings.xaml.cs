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
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked_Change_Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeLogin());
        }

        private async void Button_Clicked_Change_Email(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeEmail());
        }

        private async void Button_Clicked_Change_Password(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }

        private async void Button_Clicked_Delete_User(object sender, EventArgs e)
        {
            var del = await DisplayAlert("Are you sure?", "You will not be able to undo this action", "Ok", "Cancel");
            if (del)
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                await firebaseHelper.DeleteUser(Login.CurrentUserID);

                await DisplayAlert("You deleted your account", "The app will close now", "Ok");

                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
    }


}