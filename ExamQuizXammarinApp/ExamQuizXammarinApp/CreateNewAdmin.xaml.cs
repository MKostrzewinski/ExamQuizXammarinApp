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
	public partial class CreateNewAdmin : ContentPage
	{
        public CreateNewAdmin ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked_Admin_Add(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(txtUsername.Text) || String.IsNullOrWhiteSpace(txtPassword.Text) || String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                await DisplayAlert("Fail!", "You left empty fields", "OK");
                await Navigation.PushAsync(new CreateNewAdmin());
            }
            else
            {
                FirebaseHelper firebaseHelper = new FirebaseHelper();

                var result = await firebaseHelper.FindAdminsByLogin(txtUsername.Text);

                if (result != null)
                {
                    await DisplayAlert("Fail", "This username is not available", "OK");
                    txtUsername.Text = string.Empty;
                }
                else
                {
                    await firebaseHelper.AddAdmin(txtUsername.Text, txtPassword.Text, txtEmail.Text);
                    await DisplayAlert("Success", "Admin Added Successfully", "OK");
                    await Navigation.PushAsync(new AdminPanel());

                }
            }
        }
    }
}