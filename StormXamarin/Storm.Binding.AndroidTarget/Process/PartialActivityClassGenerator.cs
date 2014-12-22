﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Binding.AndroidTarget.Process
{
	public class PartialActivityClassGenerator : AbstractClassGenerator
	{
		public PartialActivityClassGenerator(string namespaceName, string className) : base(namespaceName, className, null)
		{
			IsPartialClass = true;
			BaseClass = null;
		}

		protected override CodeMethodReferenceExpression GetFindViewReferenceExpression(string type)
		{
			return new CodeMethodReferenceExpression(
						new CodeThisReferenceExpression(),
						"FindViewById",
						new CodeTypeReference(type));
		}

		protected override CodeExpression GetLayoutInflaterReferenceExpression()
		{
			return new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "LayoutInflater");
		}

		protected override string GetOverrideMethodName()
		{
			return "GetBindingPaths";
		}

		protected override MemberAttributes GetOverrideMethodVisibility()
		{
			return MemberAttributes.Family;
		}
	}
}