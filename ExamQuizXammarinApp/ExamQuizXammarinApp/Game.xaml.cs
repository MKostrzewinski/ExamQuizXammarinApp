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
        public string correctAnswear;
        public string category_name;
        private int random_id_value;
        private int questionsCounter = 1;
        private int scoreCounter = 0;
       
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
            Function_For_Next_Question();
        }

        private async void Function_For_Next_Question()
        {
            var min_id = await firebaseHelper.FindFirstQuestionID(category_name);  // get min question id value from chosen category
            int min_id_value = min_id.ID;
            var max_id = await firebaseHelper.FindLastQuestionID(category_name);   // // get max question id value from chosen category
            int max_id_value = max_id.ID;

            GiveRandom(min_id_value, max_id_value); // generating random id value

            var get_qustions_by_category = await firebaseHelper.GetAllQuestionsByCategory  // function which shows question based on random id taken
                (category_name, random_id_value);

            string[] answers = {get_qustions_by_category.CorrectAnswer, get_qustions_by_category.WrongAnswer1,
                                get_qustions_by_category.WrongAnswer2, get_qustions_by_category.WrongAnswer3};

            correctAnswear = answers[0];

            Random rnd = new Random();
            string[] randomAnswears = answers.OrderBy(x => rnd.Next()).ToArray(); // sorting answears randomly to array

            txt_question.Text = get_qustions_by_category.QuestionText;
            txt_Answear1.Text = randomAnswears[0];
            txt_Answear2.Text = randomAnswears[1];
            txt_Answear3.Text = randomAnswears[2];
            txt_Answear4.Text = randomAnswears[3];

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
            txt_Answear1.IsVisible = true;
            txt_Answear2.IsVisible = true;
            txt_Answear3.IsVisible = true;
            txt_Answear4.IsVisible = true;
            start_button.IsVisible = false;

        }

        private async void Button_Clicked_Answear1(object sender, EventArgs e)
        {
            if (txt_Answear1.Text.ToString() == correctAnswear)      // This condition checks if this was the correct answer
            {
                scoreCounter++; 
                questionsCounter++;
                await DisplayAlert("Correct", ":)", "OK");

                if(questionsCounter == 10)      // This condition checks if this was the last question in the series
                {
                    var result = await firebaseHelper.GetUser(Login.CurrentUserID);     // Downloading the current user's result

                    string username = result.Username;
                    string password = result.Password;
                    string email = result.Email;

                    int currentUserScore = result.Score;
                    int currentUserTotalScore = result.Totalscore;

                    currentUserScore = currentUserScore + scoreCounter;
                    currentUserTotalScore = currentUserTotalScore + scoreCounter;

                    await firebaseHelper.UpdateUserScore(Login.CurrentUserID, currentUserScore, currentUserTotalScore, username, password, email);     //Updating User's Score

                    await DisplayAlert("Your score:", scoreCounter.ToString(), "OK");
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    Function_For_Next_Question();
                }
            }
            else
            {
                questionsCounter++;
                await DisplayAlert("Wrong", ":'(", "OK");

                if (questionsCounter == 10)      // This condition checks if this was the last question in the series
                {
                    var result = await firebaseHelper.GetUser(Login.CurrentUserID);     // Downloading the current user's result

                    string username = result.Username;
                    string password = result.Password;
                    string email = result.Email;

                    int currentUserScore = result.Score;
                    int currentUserTotalScore = result.Totalscore;

                    currentUserScore = currentUserScore + scoreCounter;
                    currentUserTotalScore = currentUserTotalScore + scoreCounter;

                    await firebaseHelper.UpdateUserScore(Login.CurrentUserID, currentUserScore, currentUserTotalScore, username, password, email);     //Updating User's Score

                    await DisplayAlert("Your score:", scoreCounter.ToString(), "OK");
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    Function_For_Next_Question();
                }
            }
        }
        private async void Button_Clicked_Answear2(object sender, EventArgs e)
        {
            if (txt_Answear2.Text.ToString() == correctAnswear)
            {
                scoreCounter++;
                questionsCounter++;
                await DisplayAlert("Correct", ":)", "OK");

                if (questionsCounter == 10)
                {
                    var result = await firebaseHelper.GetUser(Login.CurrentUserID);     // Downloading the current user's result

                    string username = result.Username;
                    string password = result.Password;
                    string email = result.Email;

                    int currentUserScore = result.Score;
                    int currentUserTotalScore = result.Totalscore;

                    currentUserScore = currentUserScore + scoreCounter;
                    currentUserTotalScore = currentUserTotalScore + scoreCounter;

                    await firebaseHelper.UpdateUserScore(Login.CurrentUserID, currentUserScore, currentUserTotalScore, username, password, email);     //Updating User's Score

                    await DisplayAlert("Your score:", scoreCounter.ToString(), "OK");
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    Function_For_Next_Question();
                }
            }
            else
            {
                questionsCounter++;
                await DisplayAlert("Wrong", ":'(", "OK");

                if (questionsCounter == 10)
                {
                    var result = await firebaseHelper.GetUser(Login.CurrentUserID);     // Downloading the current user's result

                    string username = result.Username;
                    string password = result.Password;
                    string email = result.Email;

                    int currentUserScore = result.Score;
                    int currentUserTotalScore = result.Totalscore;

                    currentUserScore = currentUserScore + scoreCounter;
                    currentUserTotalScore = currentUserTotalScore + scoreCounter;

                    await firebaseHelper.UpdateUserScore(Login.CurrentUserID, currentUserScore, currentUserTotalScore, username, password, email);     //Updating User's Score

                    await DisplayAlert("Your score:", scoreCounter.ToString(), "OK");
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    Function_For_Next_Question();
                }
            }
        }

        private async void Button_Clicked_Answear3(object sender, EventArgs e)
        {
            if (txt_Answear3.Text.ToString() == correctAnswear)
            {
                scoreCounter++;
                questionsCounter++;
                await DisplayAlert("Correct", ":)", "OK");

                if (questionsCounter == 10)
                {
                    var result = await firebaseHelper.GetUser(Login.CurrentUserID);     // Downloading the current user's result

                    string username = result.Username;
                    string password = result.Password;
                    string email = result.Email;

                    int currentUserScore = result.Score;
                    int currentUserTotalScore = result.Totalscore;

                    currentUserScore = currentUserScore + scoreCounter;
                    currentUserTotalScore = currentUserTotalScore + scoreCounter;

                    await firebaseHelper.UpdateUserScore(Login.CurrentUserID, currentUserScore, currentUserTotalScore, username, password, email);     //Updating User's Score

                    await DisplayAlert("Your score:", scoreCounter.ToString(), "OK");
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    Function_For_Next_Question();
                }
            }
            else
            {
                questionsCounter++;
                await DisplayAlert("Wrong", ":'(", "OK");

                if (questionsCounter == 10)
                {
                    var result = await firebaseHelper.GetUser(Login.CurrentUserID);     // Downloading the current user's result

                    string username = result.Username;
                    string password = result.Password;
                    string email = result.Email;

                    int currentUserScore = result.Score;
                    int currentUserTotalScore = result.Totalscore;

                    currentUserScore = currentUserScore + scoreCounter;
                    currentUserTotalScore = currentUserTotalScore + scoreCounter;

                    await firebaseHelper.UpdateUserScore(Login.CurrentUserID, currentUserScore, currentUserTotalScore, username, password, email);     //Updating User's Score

                    await DisplayAlert("Your score:", scoreCounter.ToString(), "OK");
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    Function_For_Next_Question();
                }
            }
        }

        private async void Button_Clicked_Answear4(object sender, EventArgs e)
        {
            if (txt_Answear4.Text.ToString() == correctAnswear)
            {
                scoreCounter++;
                questionsCounter++;
                await DisplayAlert("Correct", ":)", "OK");

                if (questionsCounter == 10)
                {
                    var result = await firebaseHelper.GetUser(Login.CurrentUserID);     // Downloading the current user's result

                    string username = result.Username;
                    string password = result.Password;
                    string email = result.Email;

                    int currentUserScore = result.Score;
                    int currentUserTotalScore = result.Totalscore;

                    currentUserScore = currentUserScore + scoreCounter;
                    currentUserTotalScore = currentUserTotalScore + scoreCounter;

                    await firebaseHelper.UpdateUserScore(Login.CurrentUserID, currentUserScore, currentUserTotalScore, username, password, email);     //Updating User's Score

                    await DisplayAlert("Your score:", scoreCounter.ToString(), "OK");
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    Function_For_Next_Question();
                }
            }
            else
            {
                questionsCounter++;
                await DisplayAlert("Wrong", ":'(", "OK");

                if (questionsCounter == 10)
                {
                    var result = await firebaseHelper.GetUser(Login.CurrentUserID);     // Downloading the current user's result

                    string username = result.Username;
                    string password = result.Password;
                    string email = result.Email;

                    int currentUserScore = result.Score;
                    int currentUserTotalScore = result.Totalscore;

                    currentUserScore = currentUserScore + scoreCounter;
                    currentUserTotalScore = currentUserTotalScore + scoreCounter;

                    await firebaseHelper.UpdateUserScore(Login.CurrentUserID, currentUserScore, currentUserTotalScore, username, password, email);     //Updating User's Score

                    await DisplayAlert("Your score:", scoreCounter.ToString(), "OK");
                    await Navigation.PushAsync(new GameMenu());
                }
                else
                {
                    Function_For_Next_Question();
                }
            }
        }

    }
}
