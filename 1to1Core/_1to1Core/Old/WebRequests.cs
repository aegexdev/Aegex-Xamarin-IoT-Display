using Xamarin.Forms;
using RestSharp;
using System;
using System.IO;

namespace _1to1Core
{
	public class WebRequests
	{
		/// <summary>
		/// Root of page where everything will be drawn to
		/// </summary>
		StackLayout _pageRoot;
		Component   _component;

		// Deserialized JSON from APIs
		StarWarsObject _starWarsObject;
		// TODO: Model this after the JSON you are returning from your API
		RequestObject  _requestObject;

		// API authentication
		public string _domainUrl   = "http://fineos.dev.cmaeon.com/webservices";
		public string _requestUrl  = "/auth.asmx/internalLogin";
		public string _credentials = "loginName =user28%40finaeosconnect.com&password=P%40ssw0rd&rememberMe=false";
		public string _fileUrl     = "cmaeon/rocket/files.asmx/viewFile?filePath=files%5C1305%5CDemo+Instances.docx&version=1";
		public string _filePath    = @"files\1305\Demo Instances.docx";

		/// <summary>
		/// Store cookie values during first API request. Two (2) should be returned.
		/// </summary>
		public System.Collections.Generic.IList<RestResponseCookie> _listCookies;

		/// <summary>
		/// Stores binary for file pulled down from API
		/// </summary>
		public Byte[] _rawByteArray;

		/// <summary>
		/// Should return .docx at the moment, based on file pulled down from API
		/// </summary>
		public string _contentType;

		/// <summary>
		/// Name of the file downloaded from API
		/// </summary>
		public string _dlFileName;

		/// <summary>
		/// Storage location for temp files in sumulator
		/// </summary>
		 public string _localFilePath = Path.Combine(
											Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
											"docs"
										 );

		public string TempLocation
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		public string FilePath
		{
			get;
			set;
		}


		//public ComponentLibraryPage()
		//{
		//	InitializeComponent();

		//	// Web requests
		//	makeStarWarsWebRequest();
		//	makeWebRequest(_domainUrl, _requestUrl);
		//	GetWebRequestWithCookie(_domainUrl, _fileUrl);

		//	// Get ref to XAML, add person to the main page & draw it
		//	this._pageRoot = this.FindByName<StackLayout>("StackLayout_comp");
		//	this._pageRoot.Children.Add(createPerson());            // Local
		//	this._pageRoot.Children.Add(createStarWarsPerson());    // Star Wars API
		//}


		/// <summary>
		/// Create a person (StackLayout) from local data
		/// </summary>
		/// <returns>The person.</returns>
		public StackLayout createPerson()
		{
			// Create a new component from Component.cs library
			_component   =  new Component();
			var myPerson = _component.personContainer;

			// Add all of the components needed for a person
			myPerson.Children.Add(_component.nameComp (_component._name    ));
			myPerson.Children.Add(_component.roleComp (_component._role    ));
			myPerson.Children.Add(_component.photoComp(_component._imageUri));

			return myPerson;
		}


		/// <summary>
		/// Create a person (StackLayout) from Star Wars API data
		/// </summary>
		public View createStarWarsPerson()
		{
			_component   = new Component();
			var myPerson = _component.personContainer;

			// Add all of the components needed for a person
			myPerson.Children.Add(_component.nameComp (_starWarsObject.name  ));
			myPerson.Children.Add(_component.roleComp (_starWarsObject.gender));
			myPerson.Children.Add(_component.photoComp(_component._imageUri  ));

			return myPerson;
		} 


		/// <summary>
		/// Retrieve JSON data from http request
		/// </summary>
		public void makeStarWarsWebRequest()
		{
			var client  = new RestClient ("http://swapi.co/api/");
			var request = new RestRequest("people/1", Method.GET);

			// Automatically deserialize result
			IRestResponse<StarWarsObject> response2 = client.Execute<StarWarsObject>(request);
			_starWarsObject 						= response2.Data;
		}


		/// <summary>
		/// MUST MAKE POST REQUEST
		/// Cookie management: https://github.com/restsharp/RestSharp/wiki/Cookies
		/// </summary>
		public void makeWebRequest(string endPoint, string requestUrl)
		{
			var client = new RestClient(endPoint);

			var request = new RestRequest(requestUrl, Method.POST);
				request.AddHeader   ("Content-Type", "application/x-www-form-urlencoded");
				request.AddParameter("loginName"   , "user28@finaeosconnect.com"		);
				request.AddParameter("password"    , "P@ssw0rd"							);
				request.AddParameter("rememberMe"  , "false"							);

			// execute the request & store cookies for future requests
			IRestResponse  response = client.Execute(request);
			var content  = response.Content; // raw content as string
			_listCookies = response.Cookies; // RestSharp.RestResponseCookie

		}


		/// <summary>
		/// Gets the web request with cookie from a previous request
		/// </summary>
		public void GetWebRequestWithCookie(string domainUrl, string requestUrl)
		{
			var client  = new RestClient(domainUrl);
			var request = new RestRequest(requestUrl, Method.GET);

			// Build request, loop through the two (2) cookies returned.
			foreach (var sCookie in _listCookies)
			{
				request.AddParameter(sCookie.Name, sCookie.Value, ParameterType.Cookie);
				Console.WriteLine("sCookie: " + sCookie.Value);
			}
			// Hardcoded file path for demo purposes
			request.AddParameter("filePath", _filePath, ParameterType.UrlSegment);
			request.AddParameter("version" , "1"      , ParameterType.UrlSegment);

			// execute the request & use cookies from previous request
			IRestResponse response = client.Execute(request);
			var content 		= response.Content;         // raw content as string
			_rawByteArray 		= response.RawBytes;      // Byte array to be used by File Reader / Writer
			_contentType  	    = response.ContentType;   // Parsed to determine if .docx, .pdf, etc.,
			var _nameFromHeader = (string)response.Headers[5].Value; // Parse name from header
			_dlFileName  		= _nameFromHeader.Substring(_nameFromHeader.IndexOf('"')); // Remove extra content from name
			TempLocation 		= _localFilePath + FileName;  // This is the location we will reference when using the file

			// Expose properties publicly	
			FileName = _dlFileName; // Button will display this text
			FilePath = _localFilePath;

			writeFileFromBytes(TempLocation, _rawByteArray);
			Console.WriteLine("File Path: ");
			Console.WriteLine(File.ReadAllText(TempLocation));
		}


		/// <summary>
		/// Stores the file in local simulator storage 
		/// </summary>
		/// <param name="localFilePath">Local file path.</param>
		/// <param name="rawBytes">Raw bytes from http request</param>
		void writeFileFromBytes(String localFilePath, Byte[] rawBytes)
		{
			File.WriteAllBytes(localFilePath, rawBytes);
		}


	}
}
