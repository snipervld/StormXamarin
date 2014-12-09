﻿using System.Collections.Generic;
using Android.Views;

namespace Storm.Mvvm.Bindings
{
	public class BindingObject
	{
		public View TargetObject { get; set; }

		public List<BindingExpression> Expressions { get; private set; }

		public BindingObject()
		{
			Expressions = new List<BindingExpression>();
		}

		public BindingObject(View targetObject) : this()
		{
			TargetObject = targetObject;
		}

		public void AddExpression(BindingExpression expr)
		{
			Expressions.Add(expr);
		}
	}
}