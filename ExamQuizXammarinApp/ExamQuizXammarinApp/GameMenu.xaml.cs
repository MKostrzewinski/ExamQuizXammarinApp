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
    public partial class GameMenu : ContentPage
    {
        public GameMenu()
        {
            InitializeComponent();
        }
        private async void Button_Clicked_SuggestQuestion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewQuestion());
        }
    }
}