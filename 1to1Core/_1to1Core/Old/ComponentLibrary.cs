using System;
using Xamarin.Forms;

namespace _1to1Core
{
	/*  USAGE: 
     *  
	 *	// Create a person (StackLayout) from local data
	 *	StackLayout createPerson() 
	 *	{ 
	 *		// Create a new component from Component.cs library
	 *		_component   = new Component();
	 *		var myPerson = _component.personContainer;
     *
	 *		// Add all of the components needed for a person
	 *		myPerson.Children.Add(_component.nameComp (_component._name	   ));
     *		myPerson.Children.Add(_component.roleComp (_component._role	   ));
	 *		myPerson.Children.Add(_component.photoComp(_component._imageUri));
     *
	 *		return myPerson;
     *	}
	 *
	 *
	 *  // Called during Init of ComponentLibraryPage
	 *	public ComponentLibraryPage()
	 *	{
	 *		InitializeComponent();
     *
     *		// Get reference to XAML, add person to the main page & draw it
     *		this._pageRoot = this.FindByName<StackLayout>("StackLayout_comp");
     *		this._pageRoot.Children.Add(createPerson());            
	 *	}
	 */

	/// <summary>
	/// Contains small components / widgets for creating reusable UI elements
	/// </summary>
	public class Component
	{
		// TODO: Replace w/ call from database
		public string _imageUri = "https://pbs.twimg.com/profile_images/1305033822/Morbo2.jpg";
		public string _name = "Name: Morbo the Annihilator";
		public string _role = "Role: News Anchor";



		/// <summary>
		/// Empty container to hold a person's attributes: Name, Role, Image, etc.,
		/// </summary>
		public StackLayout personContainer = new StackLayout()
		{
		};


		/// <summary>
		/// Creates a Label based on the user's name
		/// </summary>
		/// <returns>Label w/ user's name</returns>
		/// <param name="name">What is the name of this person?</param>
		/// <param name="fontSize"></param>
		public Label nameComp(string name, int fontSize = 20)
		{
			var _nameComp = new Label()
			{
				Text = name,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.Black,
				Margin = new Thickness(0, 20, 0, 5),
				FontSize = fontSize
			};
			return _nameComp;
		}


		/// <summary>
		/// Creates a Label based on the user's role / position
		/// </summary>
		/// <returns>Label w/ user's role / position</returns>
		/// <param name="role">What is the role / position of this person?</param>
		/// <param name="fontSize"></param>
		public Label roleComp(string role, int fontSize = 14)
		{
			var _roleComp = new Label()
			{
				Text = role,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.Gray,
				Margin = new Thickness(0, 5, 0, 5),
				FontSize = fontSize
			};
			return _roleComp;
		}


		/// <summary>
		/// Creates a Image based on url passed in from user
		/// </summary>
		/// <returns>Image w/ user's mugshot</returns>
		/// <param name="imageUri">Mugshot of user</param>
		public Image photoComp(string imageUri)
		{
			var _photoComp = new Image()
			{
				Aspect = Aspect.AspectFit,
				Source = ImageSource.FromUri(new Uri(imageUri))
				//Source = ImageSource.FromFile("Img/morbo.jpg")
			};
			return _photoComp;
		}


	}
}

