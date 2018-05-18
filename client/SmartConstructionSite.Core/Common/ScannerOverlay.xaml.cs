using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.Common
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScannerOverlay : ContentView
	{
        private double top;
        private Page ownerPage;
        private bool firstFrame = true;

        public ScannerOverlay ()
		{
			InitializeComponent ();
            Device.StartTimer(TimeSpan.FromSeconds(1.0 / 30), Scrolling);
		}

        private bool Scrolling()
        {
            if (firstFrame)
            {
                firstFrame = false;
                line.TranslationY = -Height / 2;
                return true;
            }
            line.TranslationY = top;
            if (line.TranslationY >= frame.Height / 2)
                top = -frame.Height / 2;
            top += 3;
            return true;
        }

        private Page OwnerPage {
            get {
                if (ownerPage != null) return ownerPage;
                var element = Parent;
                while (element != null)
                {
                    if (element is Page)
                    {
                        ownerPage = (Page)element;
                        break;
                    }
                    element = element.Parent;
                }
                return ownerPage;
            }
        }
    }
}