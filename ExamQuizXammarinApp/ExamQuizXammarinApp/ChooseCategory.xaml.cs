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
            await Navigation.PushAsync(new Game(chosen_category));
        }

      
    }
}