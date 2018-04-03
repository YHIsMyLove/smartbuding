using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(RollingBoard), null,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((RollingBoard)bindable).HandleItemsSourceChanged();
                });

        public IEnumerable ItemsSource {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public RollingBoard ()
		{
			InitializeComponent ();
		}

        public void Start()
        {
            if (running || ItemsSource == null) return;
            var count = Enumerable.Count(Enumerable.Cast<object>(ItemsSource));
            if (count == 0) return;
            var msg = Enumerable.ElementAt(Enumerable.Cast<object>(ItemsSource), 0);
            label.Text = msg.ToString();
            Device.StartTimer(TimeSpan.FromSeconds(1.0 / 30), Rolling);
            running = true;
        }

        private bool Rolling()
        {
            if (Width == -1 || container.Width == -1) return true;
            if (firstFrame)
            {
                firstFrame = false;
                container.TranslationX = Width;
                return true;
            }

            container.TranslationX -= 1;
            if (container.TranslationX <= -container.Width)
            {
                container.TranslationX = Width;
                var count = Enumerable.Count(Enumerable.Cast<object>(ItemsSource));
                if (currentIdnex < count)
                {
                    var msg = Enumerable.ElementAt(Enumerable.Cast<object>(ItemsSource), currentIdnex);
                    label.Text = msg.ToString();
                    currentIdnex++;
                }
                else
                {
                    currentIdnex = 0;
                    var msg = Enumerable.ElementAt(Enumerable.Cast<object>(ItemsSource), 0);
                    label.Text = msg.ToString();
                }
            }

            return running;
        }

        public void Stop()
        {
            firstFrame = true;
            running = false;
        }

        private void HandleItemsSourceChanged()
        {
            if (ItemsSource != null)
            {
                Start();
                observableCollection = ItemsSource as INotifyCollectionChanged;
                if (observableCollection != null)
                    observableCollection.CollectionChanged += Observable_CollectionChanged;
            }
            else
            {
                Stop();
                if (observableCollection != null)
                {
                    observableCollection.CollectionChanged -= Observable_CollectionChanged;
                    observableCollection = null;
                }
            }
        }

        private void UpdateStat()
        {
            
        }

        private void Observable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //todo
        }

        private bool running;
        private bool firstFrame;
        private INotifyCollectionChanged observableCollection;
        private int currentIdnex;
    }
}