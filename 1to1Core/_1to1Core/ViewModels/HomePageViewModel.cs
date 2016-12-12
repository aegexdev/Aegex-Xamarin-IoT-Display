using System;
using System.Collections.Generic;
using System.Text;

namespace _1to1Core
{
    public class HomePageViewModel
    {
        public string UserFullname { get; set; }

        public string UserGreeting {
            get { return "Welcome " + UserFullname; }
        }

        public HomePageViewModel()
        {
            // TO DO: Need to populate this from the authentication service
            UserFullname = "FULLNAME";
        }
    }
}
