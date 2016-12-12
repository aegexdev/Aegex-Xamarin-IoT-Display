using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace _1to1Core
{
	public partial class LandingPage : ContentPage
	{
		public LandingPage()
		{
			BindingContext = new LandingPageViewModel();
			InitializeComponent();
		}
	}
}
