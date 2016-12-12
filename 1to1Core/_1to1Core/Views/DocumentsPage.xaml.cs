using System;
using Xamarin.Forms;

namespace _1to1Core
{
	/*
	 * 1) Open .docs with OpenUri
	 * 2) Use ComponentsLibrary to draw customizable components on the grid
	 */

	public partial class DocumentsPage : ContentPage
	{
		WebRequests wr;

		public DocumentsPage()
		{
			InitializeComponent();
			getDocument(); 
		}



		// TODO: Make Async
		 void getDocument()
		{
			wr = new WebRequests();
		    wr.makeWebRequest		  (wr._domainUrl, wr._requestUrl);
			wr.GetWebRequestWithCookie(wr._domainUrl, wr._fileUrl   );

			Button btn      = this.FindByName<Button>("myBtn");
				   btn.Text = wr.FileName;

			// Open doc when button is clicked 
			btn.Clicked += delegate
			{
				try
				{
					Device.OpenUri(new Uri(wr.TempLocation));
				}
				catch (Exception ex)
				{
					Console.WriteLine("Cannot open document: " + ex.Message);
				}

			};
		}
	}
}
