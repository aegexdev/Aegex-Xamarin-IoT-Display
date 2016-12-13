using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace _1to1Core
{
	public partial class DetailsPage : ContentPage
	{
		ObservableCollection<DeviceData> Objects = new ObservableCollection<DeviceData>();

		public DetailsPage(ObservableCollection<DeviceData> myCollection)
		{
			InitializeComponent();

			var lv 			   = this.FindByName<ListView>("listView");
			    lv.ItemsSource = myCollection;
		}

		void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e == null) return; // has been set to null, do not 'process' tapped event'
			Debug.WriteLine("Tapped: " + e.Item);
			((ListView)sender).SelectedItem = null; // de-select the row
		}

	}
}
