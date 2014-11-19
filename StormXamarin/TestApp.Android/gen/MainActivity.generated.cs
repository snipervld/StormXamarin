//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34014
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Android.Widget;
using Storm.Mvvm.Android.Bindings;
using System.Collections.Generic;


namespace TestApp.Android.Activities
{
	
	
	public partial class MainActivity
	{
		
		private EditText _MyInputText;
		
		private Button _MyButton;
		
		private TextView _MyTextView;
		
		private TextView _TTextView;
		
		private TextView _TCTextView;
		
		private TextView _CTextView;
		
		protected EditText MyInputText
		{
			get
			{
				if ((this._MyInputText == null))
				{
					this._MyInputText = this.FindViewById<EditText>(Resource.Id.MyInputText);
				}
				return this._MyInputText;
			}
		}
		
		protected Button MyButton
		{
			get
			{
				if ((this._MyButton == null))
				{
					this._MyButton = this.FindViewById<Button>(Resource.Id.MyButton);
				}
				return this._MyButton;
			}
		}
		
		protected TextView MyTextView
		{
			get
			{
				if ((this._MyTextView == null))
				{
					this._MyTextView = this.FindViewById<TextView>(Resource.Id.MyTextView);
				}
				return this._MyTextView;
			}
		}
		
		protected TextView TTextView
		{
			get
			{
				if ((this._TTextView == null))
				{
					this._TTextView = this.FindViewById<TextView>(Resource.Id.TTextView);
				}
				return this._TTextView;
			}
		}
		
		protected TextView TCTextView
		{
			get
			{
				if ((this._TCTextView == null))
				{
					this._TCTextView = this.FindViewById<TextView>(Resource.Id.TCTextView);
				}
				return this._TCTextView;
			}
		}
		
		protected TextView CTextView
		{
			get
			{
				if ((this._CTextView == null))
				{
					this._CTextView = this.FindViewById<TextView>(Resource.Id.CTextView);
				}
				return this._CTextView;
			}
		}
		
		protected override List<BindingObject> GetBindingPaths()
		{
			List<BindingObject> result = new List<BindingObject>();
			TestApp.Android.Converters.StringToUpperConverter rsx_0 = new TestApp.Android.Converters.StringToUpperConverter();
			BindingObject o0 = new BindingObject("MyInputText");
			result.Add(o0);
			BindingExpression e0 = new BindingExpression("text", "InputText");
			e0.Mode = BindingMode.TwoWay;
			e0.UpdateEvent = "TextChanged";
			o0.AddExpression(e0);
			BindingObject o1 = new BindingObject("MyButton");
			result.Add(o1);
			BindingExpression e1 = new BindingExpression("text", "ButtonText");
			e1.Converter = rsx_0;
			e1.ConverterParameter = "True";
			o1.AddExpression(e1);
			BindingExpression e2 = new BindingExpression("click", "ButtonCommand");
			o1.AddExpression(e2);
			BindingObject o2 = new BindingObject("MyTextView");
			result.Add(o2);
			BindingExpression e3 = new BindingExpression("text", "InputText");
			o2.AddExpression(e3);
			BindingObject o3 = new BindingObject("TTextView");
			result.Add(o3);
			BindingExpression e4 = new BindingExpression("text", "Data.Text");
			o3.AddExpression(e4);
			BindingObject o4 = new BindingObject("TCTextView");
			result.Add(o4);
			BindingExpression e5 = new BindingExpression("text", "Data.Text.Length");
			o4.AddExpression(e5);
			BindingObject o5 = new BindingObject("CTextView");
			result.Add(o5);
			BindingExpression e6 = new BindingExpression("text", "Data.Count");
			o5.AddExpression(e6);
			return result;
		}
	}
}
