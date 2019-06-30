using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamQuizXammarinApp.Database;
using ExamQuizXammarinApp.Model;

namespace ExamQuizXammarinApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccount : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public CreateAccount()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allUsers = await firebaseHelper.GetAllUsers();
            lstUsers.ItemsSource = allUsers;
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            var result = await firebaseHelper.FindUserByLogin(txtUsername.Text);

            if (result == null)
            {
                await firebaseHelper.AddUser(txtUsername.Text, txtPassword.Text, txtEmail.Text);
                await DisplayAlert("Success", "User Added Successfully", "OK");
                await Navigation.PushAsync(new Login());
            }
            else
            {
                await DisplayAlert("Fail", "This username is not available", "OK");
                txtUsername.Text = string.Empty;
                //txtId.Text = string.Empty;
                // txtUsername.Text = string.Empty;
                // txtPassword.Text = string.Empty;
                //txtEmail.Text = string.Empty;
                // var allUsers = await firebaseHelper.GetAllUsers();
                //lstUsers.ItemsSource = allUsers;
            }
        }
    
    }
}