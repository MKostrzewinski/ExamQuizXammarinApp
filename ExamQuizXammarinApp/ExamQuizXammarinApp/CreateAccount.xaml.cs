using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamQuizXammarinApp.Database;
using ExamQuizXammarinApp.NewFolder;

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
                await firebaseHelper.AddUser(Convert.ToInt32(txtId.Text), txtUsername.Text, txtPassword.Text, txtEmail.Text);
                txtId.Text = string.Empty;
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtEmail.Text = string.Empty;
                await DisplayAlert("Success", "User Added Successfully", "OK");
                var allUsers = await firebaseHelper.GetAllUsers();
                lstUsers.ItemsSource = allUsers;
            }

            private async void BtnRetrive_Clicked(object sender, EventArgs e)
            {
                var user = await firebaseHelper.GetUser(Convert.ToInt32(txtId.Text));
                if (user != null)
                {
                    txtId.Text = user.ID.ToString();
                    txtUsername.Text = user.Username;
                    txtPassword.Text = user.Password;
                    txtEmail.Text = user.Email;
                    await DisplayAlert("Success", "User Retrive Successfully", "OK");

                }
                else
                {
                    await DisplayAlert("Success", "No User Available", "OK");
                }

            }

            private async void BtnUpdate_Clicked(object sender, EventArgs e)
            {
                await firebaseHelper.UpdateUser(Convert.ToInt32(txtId.Text), txtUsername.Text, txtPassword.Text, txtEmail.Text);
                txtId.Text = string.Empty;
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtEmail.Text = string.Empty;
                await DisplayAlert("Success", "User Updated Successfully", "OK");
                var allUsers = await firebaseHelper.GetAllUsers();
                lstUsers.ItemsSource = allUsers;
            }

            private async void BtnDelete_Clicked(object sender, EventArgs e)
            {
                await firebaseHelper.DeleteUser(Convert.ToInt32(txtId.Text));
                await DisplayAlert("Success", "User Deleted Successfully", "OK");
                var allUsers = await firebaseHelper.GetAllUsers();
                lstUsers.ItemsSource = allUsers;
            }

        }
    }