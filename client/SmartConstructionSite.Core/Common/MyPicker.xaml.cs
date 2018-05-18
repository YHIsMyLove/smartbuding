using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartConstructionSite.Core.Common
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyPicker : ContentView
	{
        public event EventHandler SelectedIndexChanged;

        public MyPicker()
        {
            InitializeComponent();
            label.Text = Title;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            //HeightRequest = label.Height + 4;
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            label.Text = Title;
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(MyPicker), null,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((MyPicker)bindable).HandleItemsSourceChanged();
                });

        private void HandleItemsSourceChanged()
        {
            if (ItemsSource == null)
            {
                label.Text = Title;
                if (observableCollection != null)
                {
                    observableCollection.CollectionChanged -= Observable_CollectionChanged;
                    observableCollection = null;
                }
            }
            else
            {
                observableCollection = ItemsSource as INotifyCollectionChanged;
                if (observableCollection != null)
                    observableCollection.CollectionChanged += Observable_CollectionChanged;
            }
        }

        private void Observable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!ItemsSource.GetEnumerator().MoveNext())
            {
                label.Text = Title;
                SelectedItem = null;
            }
        }

        public IEnumerable ItemsSource {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MyPicker), null, BindingMode.TwoWay,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((MyPicker)bindable).HandleSelectedItemChanged(); });

        private void HandleSelectedItemChanged()
        {
            if (SelectedItem != null)
                label.Text = SelectedItem.ToString();
            else
                label.Text = "无";
        }

        public object SelectedItem {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(MyPicker), "");

        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            if (OwnerPage == null)
            {
                System.Diagnostics.Debug.WriteLine("MyPicker: OwnerPage is null");
                return;
            }
            if (busy) return;
            busy = true;
            List<string> buttons = new List<string>();
            if (ItemsSource != null)
            {
                foreach (var item in ItemsSource)
                {
                    if (item != null)
                        buttons.Add(item.ToString());
                    else
                        buttons.Add("无");
                }
            }
            var result = await OwnerPage.DisplayActionSheet(Title, null, null, buttons.ToArray());
            busy = false;
            if (result == null) return;
            int selectedIndex = buttons.IndexOf(result);
            int index = 0;
            foreach (var item in ItemsSource)
            {
                if (index == selectedIndex)
                {
                    SelectedItem = item;
                    break;
                }
                index++;
            }
            HandleSelectedItemChanged();
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

        private Page ownerPage;
        private bool busy;
        private INotifyCollectionChanged observableCollection;
    }
}