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
	public partial class VerifyQuestions : ContentPage
	{
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public VerifyQuestions ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked_Take_Question(object sender, EventArgs e)
        {
            var take_suggested_question_id = await firebaseHelper.FindLastSuggestedQuestionsID();
            int suggested_question_id = take_suggested_question_id.ID;
            var question = await firebaseHelper.GetQuestion(suggested_question_id);
           
            txt_question_category.Text = question.Category;
            txt_question.Text = question.QuestionText;
            txt_correct_answear.Text = question.QuestionText;
            txt_wrong_answear1.Text = question.WrongAnswer1;
            txt_wrong_answear2.Text = question.WrongAnswer2;
            txt_wrong_answear3.Text = question.WrongAnswer3;
            

        }
        private async void Button_Clicked_Delete_Question(object sender, EventArgs e)
        {
            var take_suggested_question_id = await firebaseHelper.FindLastSuggestedQuestionsID();
            int suggested_question_id = take_suggested_question_id.ID;

            await firebaseHelper.DeleteSuggestedQuestion(suggested_question_id);
            await DisplayAlert("Added Question", "Question has been deleted", "ok");
            await Navigation.PushAsync(new VerifyQuestions());

        }

            private async void Button_Clicked_Add_Question(object sender, EventArgs e)
            {
            var take_suggested_question_id = await firebaseHelper.FindLastSuggestedQuestionsID();
            int suggested_question_id = take_suggested_question_id.ID;
            await firebaseHelper.AddQuestion(txt_question_category.Text,
                txt_question.Text, txt_correct_answear.Text, txt_wrong_answear1.Text,
                txt_wrong_answear2.Text, txt_wrong_answear3.Text);
            await DisplayAlert("Added Question", "Question has been added", "ok");
            await firebaseHelper.DeleteSuggestedQuestion(suggested_question_id);
            await Navigation.PushAsync(new VerifyQuestions());
        }

    }
}