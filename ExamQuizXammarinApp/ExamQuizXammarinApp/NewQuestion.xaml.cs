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
    public partial class NewQuestion : ContentPage
    {
        public NewQuestion()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Submit(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace((string)CategoryPicker.SelectedItem) 
                || String.IsNullOrWhiteSpace(entryQuestion.Text) || String.IsNullOrWhiteSpace(entryCorrectAnswer.Text) 
                || String.IsNullOrWhiteSpace(entryWrongAnswer1.Text) || String.IsNullOrWhiteSpace(entryWrongAnswer2.Text) 
                || String.IsNullOrWhiteSpace(entryWrongAnswer3.Text))
            {
                await DisplayAlert("Fail!", "You left empty fields", "OK");
                await Navigation.PushAsync(new NewQuestion());
            }
            else
            {
                string category = (string)CategoryPicker.SelectedItem;
                FirebaseHelper firebaseHelper = new FirebaseHelper();
                await firebaseHelper.AddSuggestedQuestion(category, entryQuestion.Text, entryCorrectAnswer.Text, entryWrongAnswer1.Text, entryWrongAnswer2.Text, entryWrongAnswer3.Text);
                await DisplayAlert("Succes", "You suggested a question", "OK");
                await Navigation.PushAsync(new GameMenu());
            }

        }
    }
}