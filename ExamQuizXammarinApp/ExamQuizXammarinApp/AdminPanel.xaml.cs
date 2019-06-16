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

       

        private async void Button_Clicked_Admin_Add(object sender, EventArgs e)
        {
            var result = firebaseHelper.FindAdminsByLogin(txtUsername.Text);
            
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