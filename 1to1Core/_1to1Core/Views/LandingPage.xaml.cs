
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin.Forms;
using RestSharp;
using Newtonsoft.Json;

namespace _1to1Core 
{

	public partial class LandingPage : ContentPage
	{
		public static ObservableCollection<DeviceData> ocDeviceData = new ObservableCollection<DeviceData>();
		string _domain   = "https://aegexdemo.azurewebsites.net/api/GetLatestSensorValues?code=h6FgTimqp7kK2XmjPgmsYqGFCafTV5AG5X8ktA9XBNTK1gMJOUUd4g==";
		string _endPoint = "";

		List<DeviceData> dData;

		string myJson = @"[{""Id"":35052,""DeviceId"":""Device01"",""SensorType"":""gasSensor"",""SensorValue"":""High"",""OutputTime"":""2016-12-13T10:58:20""},{""Id"":35055,""DeviceId"":""Device01"",""SensorType"":""lightSensor"",""SensorValue"":""559"",""OutputTime"":""2016-12-13T10:58:21""},{""Id"":35053,""DeviceId"":""Device01"",""SensorType"":""flameSensor"",""SensorValue"":""High"",""OutputTime"":""2016-12-13T10:58:20""},{""Id"":34757,""DeviceId"":""Device01"",""SensorType"":""tempSensor"",""SensorValue"":""Low"",""OutputTime"":""2016-12-13T09:46:39""},{""Id"":35054,""DeviceId"":""Device01"",""SensorType"":""knockSensor"",""SensorValue"":""High"",""OutputTime"":""2016-12-13T10:58:20""}]";

		public LandingPage()
		{
			InitializeComponent();

			// Create dummy data
			// TODO: Remove this when we can return data from API
			//ocDeviceData.Add(new DeviceData()
			//{
			//	Id = 0,
			//	DeviceId = "MyDevice",
			//	SensorType = "MyType",
			//	SensorValue = "MyValue",
			//	OutputTime = DateTime.Now
			//});

			//ocDeviceData.Add(new DeviceData()
			//{
			//	Id = 1,
			//	DeviceId = "MyDeviceOne",
			//	SensorType = "MyType One",
			//	SensorValue = "MyValueOne",
			//	OutputTime = DateTime.Now
			//});

			// Set data here
			getWebRequest(_domain, _endPoint);

			// Set data to the listview 
			var lv 			   = this.FindByName<ListView>("listView");
			    lv.ItemsSource = ocDeviceData;

			// Tell hockey app that we used landing page
			// TODO: Hockeyapp SDK has issues with iOS 10.x simulator
			HockeyApp.MetricsManager.TrackEvent("Loaded Landing Page");
		}


		/// <summary>
		/// Grab JSON data from SQL database, via Azure Function
		/// </summary>
		void getWebRequest(string domain, string endPoint)
		{
			var client  = new RestClient (domain);
			var request = new RestRequest(endPoint, Method.GET);

			// Automatically deserialize result
			IRestResponse<DeviceData> response = client.Execute<DeviceData>(request);
			Debug.WriteLine(response.Content);

			// Crazy manipulation. Can probably get rid of this
			//var thisData = JsonConvert.DeserializeObject<List<DeviceData>>(response.Content);
			//var newString = @"" + thisData;
			//var finalString = JsonConvert.DeserializeObject<List<DeviceData>>(newString);
			//Debug.WriteLine(newString[0]);
			//Debug.WriteLine(finalString[0].DeviceId);

			ocDeviceData = JsonConvert.DeserializeObject<ObservableCollection<DeviceData>>(myJson);
			Debug.WriteLine(ocDeviceData[0].DeviceId);

			//dData = JsonConvert.DeserializeObject<List<DeviceData>>(myJson);
			//Debug.WriteLine(dData[0].DeviceId);
		}



		void postWebRequest(string domain, string endPoint)
		{
			var client  = new RestClient (domain);
			var request = new RestRequest(endPoint, Method.POST);
			// TODO: Add header  + Params here if necessary

			// execute the request & store cookies for future requests
			IRestResponse response = client.Execute(request);
			var content = response.Content; // raw content as string
		}


		/// <summary>
		/// navigate to details page 
		/// </summary>
		void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e == null) return; // has been set to null, do not 'process' tapped event'
			((ListView)sender).SelectedItem = null; // de-select the row
			App.NavigationPage.Navigation.PushAsync(new DetailsPage(ocDeviceData));
		}

	}
}

