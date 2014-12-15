﻿using System;

namespace Storm.Mvvm.Inject
{
	public interface IContainer
	{
		void RegisterFactory<TClass>(Func<IContainer, TClass> factory);
		
		void RegisterFactory<TInterface, TClass>(Func<IContainer, TClass> factory) where TClass : TInterface;
		
		void RegisterInstance<TClass>(TClass _object);
		
		void RegisterInstance<TInterface, TClass>(TClass _object) where TClass : TInterface;
		
		TClass Resolve<TClass>();
	}
}
