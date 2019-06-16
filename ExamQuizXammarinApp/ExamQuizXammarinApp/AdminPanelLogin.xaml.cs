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
	public partial class AdminPanelLogin : ContentPage
	{
		public AdminPanelLogin ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked_AdminLogin(object sender, EventArgs e)
        {
            
            if (String.IsNullOrWhiteSpace(entryLogin.Text) || String.IsNullOrWhiteSpace(entryPassword.Text))
            {
                await DisplayAlert("Fail!", "You left empty fields", "OK");
                await Navigation.PushAsync(new AdminPanelLogin());
            }
            else
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                var result = await firebaseHelper.FindAdminByLoginAndPassword(entryLogin.Text, entryPassword.Text);
                if (result != null)
                {
                    await Navigation.PushAsync(new AdminPanel());
                }
                else
                {
                    await DisplayAlert("Fail!", "Wrong login or password", "OK");
                    await Navigation.PushAsync(new AdminPanelLogin());
                }
            }
            
            
        }

    }
}