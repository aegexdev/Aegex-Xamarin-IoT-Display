using System;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace _1to1Core
{
	public class LandingPageViewModel
	{
		public ObservableCollection<objClass> Objects { get; set; }


		public LandingPageViewModel()
		{
			
			this.Objects = new ObservableCollection<objClass>();
			this.Objects.Add(new objClass()
			{
				Name = "Dave",
				Age = 29,
				Location = "Philadelphia, PA"
			});

			this.Objects.Add(new objClass()
			{
				Name = "Morbo the Annihilator",
				Age = 10,
				Location = "Atlanta, GA"
			});

			Debug.WriteLine(Objects[0].Name);


		}

	}
}
