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
	public partial class Game : ContentPage
	{
        public string category_name;
        private int random_id_value;
       
        FirebaseHelper firebaseHelper = new FirebaseHelper();
   
        
        public Game ()
		{
			InitializeComponent ();
		}
        public Game(string name_of_category)
        {
            InitializeComponent();
            category_name = name_of_category; 

        }

        private async void Button_Clicked_Next_Question(object sender, EventArgs e)
        {

            

            var min_id = await firebaseHelper.FindFirstQuestionID(category_name);  // get min question id value from chosen category
            int min_id_value = min_id.ID;
            var max_id = await firebaseHelper.FindLastQuestionID(category_name);   // // get max question id value from chosen category
            int max_id_value = max_id.ID;

            GiveRandom(min_id_value, max_id_value); // generating random id value

            var get_qustions_by_category = await firebaseHelper.GetAllQuestionsByCategory  // function which shows question based on random id taken
                (category_name, random_id_value);

            txt_question_category.Text = get_qustions_by_category.Category;
            txt_question.Text = get_qustions_by_category.QuestionText;
            txt_correct_answear.Text = get_qustions_by_category.CorrectAnswer;
            txt_wrong_answear1.Text = get_qustions_by_category.WrongAnswer1;
            txt_wrong_answear2.Text = get_qustions_by_category.WrongAnswer2;
            txt_wrong_answear3.Text = get_qustions_by_category.WrongAnswer3;

            BindingButtonsVisibility(); //binding buttons visibility

        }
        private int GiveRandom(int min, int max)
        {
           
            Random random = new Random();    // function which generate random ids based on (min and max id_value)
            random_id_value = random.Next(min, max+1);
            return random_id_value;
           
        }
        private void BindingButtonsVisibility()    // function which bind buttons visibility
        {
            txt_correct_answear.IsVisible = true;
            txt_wrong_answear1.IsVisible = true;
            txt_wrong_answear2.IsVisible = true;
            txt_wrong_answear3.IsVisible = true;
            next_question_btn.IsVisible = true;
            start_button.IsVisible = false;

        }
       
    }
}
