﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.Common
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RollingBoard : ContentView
	{
		public RollingBoard ()
		{
			InitializeComponent ();
		}
	}
}