using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace _1to1Core
{
	public partial class LandingPage : ContentPage
	{

		public ObservableCollection<objClass> Objects = new ObservableCollection<objClass>();

		public LandingPage()
		{
			//BindingContext = new LandingPageViewModel();
			InitializeComponent();

			var lv = this.FindByName<ListView>("listView");
			var cs = this.FindByName<CustomCell>("customCell");

			Objects.Add(new objClass()
			{
				Name     = "Dave",
				Age      = 29,
				Location = "Philadelphia, PA"
			});

			Objects.Add(new objClass()
			{
				Name = "Morbo the Annihilator",
				Age = 10,
				Location = "Atlanta, GA"
			});

			//lv.ItemsSource = new string[]{
			//  "mono",
			//  "monodroid",
			//  "monotouch",
			//  "monorail",
			//  "monodevelop",
			//  "monotone",
			//  "monopoly",
			//  "monomodal",
			//  "mononucleosis"
			//};

			// This does the same thing
			// https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/data-and-databinding/
		    lv.ItemsSource = Objects;
			//this.BindingContext = Objects[0].Name;

		}


		void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e == null) return; // has been set to null, do not 'process' tapped event'
			Debug.WriteLine("Tapped: " + e.Item);
			((ListView)sender).SelectedItem = null; // de-select the row
		}


		void OnItemSelected(object o, EventArgs e)
		{
			Console.WriteLine("OnItemSelected");

		}
	}
}
