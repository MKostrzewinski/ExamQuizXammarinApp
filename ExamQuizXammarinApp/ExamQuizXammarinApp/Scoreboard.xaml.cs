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
	public partial class Scoreboard : ContentPage
	{
		public Scoreboard ()
		{
            InitializeComponent ();
            GetScoreboard();

        }

        private async void GetScoreboard()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();

            var result = await firebaseHelper.GetAllUsersOrderedByScore();

            lst.ItemsSource = result;

        }

    }
}