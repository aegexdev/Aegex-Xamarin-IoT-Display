using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Data.SqlClient;

namespace _1to1Core
{
	public partial class LandingPage : ContentPage
	{

		public ObservableCollection<objClass> Objects = new ObservableCollection<objClass>();

		public LandingPage()
		{
			InitializeComponent();

			Objects.Add(new objClass()
			{
				Id = 0,
				DeviceId = "MyDevice",
				SensorType = "MyType",
				SensorValue = "MyValue",
				OutputTime = DateTime.Now
			});

			Objects.Add(new objClass()
			{
				Id = 1,
				DeviceId = "MyDeviceOne",
				SensorType = "MyTypeOne",
				SensorValue = "MyValueOne",
				OutputTime = DateTime.Now
			});

			var lv 			   = this.FindByName<ListView>("listView");
				lv.ItemsSource = Objects;

			// Tell hockey app that we used landing page
			HockeyApp.MetricsManager.TrackEvent("Loaded Landing Page");
		}



		void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e == null) return; // has been set to null, do not 'process' tapped event'
			Debug.WriteLine("Tapped: " + e.Item);
			((ListView)sender).SelectedItem = null; // de-select the row
			App.NavigationPage.Navigation.PushAsync(new DetailsPage());

		}


		void OnItemSelected(object o, EventArgs e)
		{
			Console.WriteLine("OnItemSelected");

		}
	}
}
