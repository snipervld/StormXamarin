//------------------------------------------------------------------------------
// This file was generated by binding preprocessing system for Android
//------------------------------------------------------------------------------

using Android.Widget;
using Storm.Mvvm.Bindings;
using System.Collections.Generic;


namespace TestApp.Android.Activities
{
	
	
	public partial class SecondActivity
	{
		
		private TextView _GreetingText;
		
		protected TextView GreetingText
		{
			get
			{
				if ((this._GreetingText == null))
				{
					this._GreetingText = this.FindViewById<TextView>(Resource.Id.GreetingText);
				}
				return this._GreetingText;
			}
		}
		
		protected override List<BindingObject> GetBindingPaths()
		{
			List<BindingObject> result = new List<BindingObject>();
			BindingObject o0 = new BindingObject("GreetingText");
			result.Add(o0);
			BindingExpression e0 = new BindingExpression("text", "Greetings");
			o0.AddExpression(e0);
			return result;
		}
	}
}
