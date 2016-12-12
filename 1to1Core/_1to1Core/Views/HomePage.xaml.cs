using System;
using Xamarin.Forms;

namespace _1to1Core
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			BindingContext = new HomePageViewModel();
			InitializeComponent ();

            imgLogo.Source = CrossHelper.GetOSFullImagePath("finaeos_logo_dark_600px.png");

            InitializeWidgetGrid();    
        }



        private void InitializeWidgetGrid(int widgetCount = 9)
        {
            int WidgetCount = widgetCount; // This will come from the server
            Button btnWidget;


			// Start in top left corner
            int row = 0, col = 0;

            for (int i = 0; i < WidgetCount; i++)
            {

                btnWidget = new Button();

                btnWidget.Image = CrossHelper.GetOSFullImagePath("notes.png");
#if __ANDROID__
                btnWidget.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button));
#else
                btnWidget.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button));
#endif
           
                btnWidget.BackgroundColor = Color.FromHex("F2F2F2");  // Very pale gray

				// For testing: First icon will be hardcoded as "Docs"
				if (i == 0)
				{
					btnWidget.Text     = "Docs";
					btnWidget.Clicked += GoToDocumentPage;
				}
				else 
				{
					btnWidget.Text = "Notes";
				}

                WidgetGrid.Children.Add(btnWidget, col, row);
                col++;

				// If column contains more than 3 objects, create a new row
                if (col > 2)
                {
                    col = 0;
                    row++;
                }             
            }
        }


		/// <summary>
		/// Navigates to document page
		/// </summary>
		/// <param name="o">Objects</param>
		/// <param name="e">EventArgs</param>
		private void GoToDocumentPage(object o, EventArgs e) 
		{
			App.NavigationPage.Navigation.PushAsync(new DocumentsPage());
			App.MenuIsPresented = false;
		}


    }
}
