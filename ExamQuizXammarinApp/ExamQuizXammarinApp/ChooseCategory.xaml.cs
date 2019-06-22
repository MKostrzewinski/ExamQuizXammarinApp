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
	public partial class ChooseCategory : ContentPage
	{
        FirebaseHelper firebaseHelper = new FirebaseHelper();
		public ChooseCategory ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked_Choose_Category(object sender, EventArgs e)
        {

       
            string chosen_category =  CategoryPicker.SelectedItem.ToString();  // get category chose by user

            var min_id = await firebaseHelper.FindFirstQuestionID(chosen_category);  // get min question id value from chosen category
            int min_id_value = min_id.ID;
            var max_id = await firebaseHelper.FindLastQuestionID(chosen_category);   // // get max question id value from chosen category
            int max_id_value = max_id.ID;
            Random random = new Random(); 
            int value = random.Next(min_id_value, max_id_value+1);  // function which generate random ids based on (min and max id_value)
           

            var get_qustions_by_category = await firebaseHelper.GetAllQuestionsByCategory  // function which shows question based on random id taken
                (chosen_category, value);
          





             txt_question_category.Text = get_qustions_by_category.Category;
                txt_question.Text = get_qustions_by_category.QuestionText;
                txt_correct_answear.Text = get_qustions_by_category.CorrectAnswer;
                txt_wrong_answear1.Text = get_qustions_by_category.WrongAnswer1;
                txt_wrong_answear2.Text = get_qustions_by_category.WrongAnswer2;
                txt_wrong_answear3.Text = get_qustions_by_category.WrongAnswer3;

            
            

        }

      
    }
}