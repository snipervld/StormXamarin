﻿using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Storm.Mvvm;
using Storm.Mvvm.Bindings;
using TestApp.Android.Converters;

namespace TestApp.Android.Activities
{
	[Activity(Label = "TestApp.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public partial class MainActivity : ActivityBase
	{
		private string _myString;

		private Random _generator = new Random((int)DateTime.Now.Ticks);

		[Binding("InputText", Converter = typeof(StringToUpperConverter))]
		public string MyString
		{
			get { return _myString; }
			set
			{
				_myString = value;
				Toast.MakeText(this, "I just changed !", ToastLength.Short);
			}
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			SetViewModel(TestApp.Android.CompositionRoot.Container.ViewModelsLocator.MainPageViewModel);
			
			
		}
	}
}

