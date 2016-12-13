/* Dave Voyles, Microsoft Corp. 2016
 * www.Dave Voyles.com | Twitter.com/DaveVoyles
 */
using System.Collections.ObjectModel;
using Xamarin.Forms;
using RestSharp;
using Newtonsoft.Json;

namespace _1to1Core 
{

	public partial class LandingPage : ContentPage
	{
		/// <summary>
		/// Store deserialized JSON from Azure Function here
		/// </summary>
		public static ObservableCollection<DeviceData> ocDeviceData = new ObservableCollection<DeviceData>();
		//string _domain   = "https://aegexdemo.azurewebsites.net/api/GetLatestSensorValues?code=h6FgTimqp7kK2XmjPgmsYqGFCafTV5AG5X8ktA9XBNTK1gMJOUUd4g==";
		string _sDomain  = "https://jmr-aeg-01.azurewebsites.net/api/GetLatestSensorData?code=nwvgkOZ0nIznS2lTNb6W9ERZZwfG7zxpA0uAgavigVJyKG0iFYeivQ==";
		string _endPoint = "";


		public LandingPage()
		{
			InitializeComponent();

			// Set data here
			getWebRequest(_sDomain, _endPoint);

			// Set data to the listview 
			var lv 			   = this.FindByName<ListView>("listView");
			    lv.ItemsSource = ocDeviceData;

			// Tell hockey app that we used landing page
			// TODO: Hockeyapp SDK has issues with iOS 10.x simulator
			HockeyApp.MetricsManager.TrackEvent("Loaded Landing Page");
		}

		/// <summary>
		/// Return JSON from Azure Function 
		/// </summary>
		/// <param name="sDomain">Root URL to pull JSON from</param>
		/// <param name="sEndPoint">[Optional], used if you want to point toward a more specific URL</param>
		void getWebRequest(string sDomain, string sEndPoint)
		{
			var client  = new RestClient (sDomain);
			var request = new RestRequest(sEndPoint, Method.GET);

			// Deserialize JSON result
			IRestResponse<DeviceData> response = client.Execute<DeviceData>(request);
			// Store deserialized result in an Observable Collection
			ocDeviceData  = JsonConvert.DeserializeObject<ObservableCollection<DeviceData>>(response.Content);
		}


		/// <summary>
		/// navigate to details page and pass in static OC of serialized DeviceData
		/// </summary>
		void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e == null) return; // has been set to null, do not 'process' tapped event'
			((ListView)sender).SelectedItem = null; // de-select the row
			App.NavigationPage.Navigation.PushAsync(new DetailsPage(ocDeviceData));
		}

	}
}

