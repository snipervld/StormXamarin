﻿using System.Collections.Generic;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Commands;
using Storm.Mvvm.Inject;
using Storm.Mvvm.Services;

namespace TestApp.Business.ViewModels
{
	public class DataContainer : NotifierBase
	{
		private string _text;
		private int _count;
		private DataContainer _data;

		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}

		public int Count
		{
			get { return _count; }
			set { SetProperty(ref _count, value); }
		}

		public DataContainer Data
		{
			get { return _data; }
			set { SetProperty(ref _data, value); }
		}
	}

	public class ChildrenDataContainer : DataContainer
	{
		public int Id { get; set; }
	}

    public class MainPageViewModel : ViewModelBase
    {
		private string _buttonText = "";
		private int _counter = 0;
	    private string _inputText;
	    private DataContainer _data;
	    private string _pushText = "";
		private List<DataContainer> _myCollection = new List<DataContainer>()
		{
			new DataContainer() { Text = "Possible 1"},
			new ChildrenDataContainer() { Text = "Possible 2"}
		};

	    private DataContainer _dataInput;


		public List<DataContainer> MyCollection
	    {
		    get { return _myCollection; }
	    }

	    public ColorContainer ColorStatic
	    {
			get { return ColorPickerViewModel.ColorStatic; }
	    }

	    public string PushText
	    {
			get { return _pushText; }
			set { SetProperty(ref _pushText, value); }
	    }

	    public DataContainer Selected { get; set; }

	    public int Color { get; set; }	

	    public string InputText
	    {
			get { return _inputText; }
			set
			{
				SetProperty(ref _inputText, value);
			}
	    }

		public string ButtonText
		{
			get { return _buttonText; }
			set { SetProperty(ref _buttonText, value); }
		}

	    public DataContainer Data
	    {
			get { return _data; }
			private set { SetProperty(ref _data, value); }
	    }

	    public DataContainer DataInput
	    {
			get { return _dataInput; }
			private set { SetProperty(ref _dataInput, value); }
	    }

		public ICommand ButtonCommand { get; private set; }

		public ICommand PushAlertCommand { get; private set; }

		public MainPageViewModel()
		{
			Data = new DataContainer();
			ButtonCommand = new DelegateCommand(ButtonAction);
			PushAlertCommand = new DelegateCommand(PushAlertAction);
			ButtonText = "Hello world !!!!";
			PushText = "ALERT !!!";

			ILocalizationService localizationService = LazyResolver<ILocalizationService>.Service;
			string name = localizationService.GetString("Hello");
			ButtonText = name;
		}

	    private async void PushAlertAction()
	    {
		    if (DataInput == null)
		    {
			    DataInput = new DataContainer();
		    }
		    else
		    {
			    DataInput.Data = new DataContainer();
		    }
		    //string imagePicker = await LazyResolver<IImagePickerService>.Service.LaunchImagePickerAsync();

			//LoggerService.Log("Image picked with async pattern : " + imagePicker, MessageSeverity.Critical);

		    //NavigationService.Navigate(Views.ADAPTER);
		    //LazyResolver<IMessageDialogService>.Service.Show(Dialogs.MAIN);
		    //Container.Resolve<IMessageDialogService>().Show(Dialogs.COLOR_PICKER);
		    //, new Dictionary<string, object>()
		    //{
		    //	{"LabelText", "Default value for label text"}
		    //});
	    }

	    private void ButtonAction()
		{
			_counter++;

			Data.Count = _counter;
			Data.Text = "Plop x " + _counter;

			ButtonText = string.Format("You've clicked {0} times on this button", _counter);

			NavigationService.Navigate(Views.SECOND, new Dictionary<string, object>(){{"Greetings", "Plop !"}});
		}
    }
}
