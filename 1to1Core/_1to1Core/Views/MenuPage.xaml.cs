﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace _1to1Core
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{
            BindingContext = new MenuPageViewModel();
            Title = "Menu";
            InitializeComponent ();
		}
	}
}
