﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Storm.Binding.Android.Data;

namespace Storm.Binding.Android.Process
{
	class InformationReader
	{
		public List<ActivityViewInfo> ActivityViewInformations { get; private set; }

		public InformationReader(string filename)
		{
			JsonSerializer serializer = new JsonSerializer();

			StreamReader re = new StreamReader(filename);
			JsonTextReader reader = new JsonTextReader(re);

			ActivityViewInfoCollection input = serializer.Deserialize<ActivityViewInfoCollection>(reader);

			ActivityViewInformations = input.list;

			string baseDir = Path.GetDirectoryName(filename);
			//rewrite all path using baseDir
			foreach(ActivityViewInfo info in ActivityViewInformations)
			{
				info.Activity.InputFile = Path.Combine(baseDir, info.Activity.InputFile);
				info.Activity.OutputFile = Path.Combine(baseDir, info.Activity.OutputFile);
				info.View.InputFile = Path.Combine(baseDir, info.View.InputFile);
				info.View.OutputFile = Path.Combine(baseDir, info.View.OutputFile);
			}
		}
	}
}
