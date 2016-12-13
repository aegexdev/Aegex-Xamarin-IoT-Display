using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;


namespace _1to1Core
{
	public class TodoItem
	{
		public string Id { get; set; }
		public string Text { get; set; }
	}

	public class myTabble
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string name { get; set; }

		[JsonProperty(PropertyName = "age")]
		public int age { get; set; }

		[JsonProperty(PropertyName = "fname")]
		public string fname { get; set; }
	}

	public partial class LandingPage : ContentPage
	{
		// Dummy data that we are inserting into the view
		public ObservableCollection<DeviceData> Objects = new ObservableCollection<DeviceData>();
		// Mobile service connection
		public static MobileServiceClient MobileService = new MobileServiceClient("https://aegex-ms.azurewebsites.net");
		// Dummy test item for mobile service
		TodoItem item = new TodoItem { Text = "Awesome item" };


		public LandingPage()
		{
			InitializeComponent();

			// Init mobile services 
			CurrentPlatform.Init();


			// Create dummy data
			Objects.Add(new DeviceData()
			{
				Id = 0,
				DeviceId = "MyDevice",
				SensorType = "MyType",
				SensorValue = "MyValue",
				OutputTime = DateTime.Now
			});

			Objects.Add(new DeviceData()
			{
				Id = 1,
				DeviceId = "MyDeviceOne",
				SensorType = "MyTypeOne",
				SensorValue = "MyValueOne",
				OutputTime = DateTime.Now
			});

			// Set items to the listview 
			var lv 			   = this.FindByName<ListView>("listView");
				lv.ItemsSource = Objects;

			// Tell hockey app that we used landing page
			HockeyApp.MetricsManager.TrackEvent("Loaded Landing Page");
		}




		void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e == null) { return; } 			    // has been set to null, do not 'process' tapped event'
			((ListView)sender).SelectedItem = null; // de-select the row
			AsyncTaskHandler();	
		}

		// Allows us to use Async in events
		// https://blogs.msdn.microsoft.com/pfxteam/2012/02/11/building-async-coordination-primitives-part-3-asynccountdownevent/
		// public void SendToAzureAsync() { return AsyncTaskHandler(); }


		// TODO: Make static?
		async void AsyncTaskHandler()
		{
			try
			{
				Console.WriteLine("Sending to Azure");
				await MobileService.GetTable<TodoItem>().InsertAsync(item);
				Console.WriteLine("SENT to Azure");
			}
			catch (Exception ex)
			{
				Console.WriteLine("ERROR: " + ex);
			}

		}


		// https://stackoverflow.com/questions/21080738/retrieve-single-value-from-azure-mobile-services-data-table
		//async Task GetFromAzure() { 
		//	IMobileServiceTableQuery<todoTable> query = todoTable.Where(t => t.fname == "[Your Name]");
		//	var res = await query.ToListAsync();
		//	var item = res.First();
		//	//fName is a string in your object MyTable just get it like this
		//	string fname = item.fname;
		//}


		/// <summary>
		/// navigate to details page 
		/// </summary>
		//void OnItemTapped(object sender, ItemTappedEventArgs e)
		//{
		//	if (e == null) return; // has been set to null, do not 'process' tapped event'
		//	Debug.WriteLine("Tapped: " + e.Item);
		//	((ListView)sender).SelectedItem = null; // de-select the row
		//	App.NavigationPage.Navigation.PushAsync(new DetailsPage());
		//}

	}
}
