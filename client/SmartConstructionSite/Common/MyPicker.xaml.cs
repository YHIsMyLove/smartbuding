using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartConstructionSite.Common
{
    public partial class MyPicker : ContentView
    {
        public event EventHandler SelectedIndexChanged;

        public MyPicker()
        {
            InitializeComponent();
            label.Text = Title;
        }

		protected override void OnParentSet()
		{
            base.OnParentSet();
            label.Text = Title;
		}

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList<string>), typeof(MyPicker), null);

        public IList<string> ItemsSource
        {
            get { return (IList<string>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MyPicker), null, BindingMode.TwoWay,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((MyPicker)bindable).UpdateLabel(); });

        private void UpdateLabel()
        {
            if (SelectedItem != null)
                label.Text = SelectedItem.ToString();
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(MyPicker), "");
        
        public string Title
        {
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
                        buttons.Add("item is null");
                }
            }
            var result = await OwnerPage.DisplayActionSheet(Title, null, null, buttons.ToArray());
            busy = false;
            if (result == null) return;
            SelectedItem = result;
            UpdateLabel();
        }

        private Page OwnerPage
        {
            get
            {
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
    }
}

