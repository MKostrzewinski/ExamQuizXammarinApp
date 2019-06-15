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
		public GameMenu ()
		{
			InitializeComponent ();
		}
        /*
         * 
         * 
                    <ContentView BackgroundColor="Blue">
                        <Button Text="New Game" BackgroundColor="Black"/>
                    </ContentView>
                    <ContentView BackgroundColor="Blue">
                        <Button Text="set" BackgroundColor="Black"/>
                    </ContentView>
                    <ContentView BackgroundColor="Blue">
                        <Button Text="scrbrd" BackgroundColor="Black"/>
                    </ContentView>
                    */
	}
}