using SmartConstructionServices.Account.Services;
using SmartConstructionServices.Account.ViewModels;
using SmartConstructionServices.Common;
using SmartConstructionSite.Account;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartConstructionSite
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Appearing += MainPage_Appearing;
            ((UserMainViewModel)Master.BindingContext).PropertyChanged += UserMainViewModel_PropertyChanged;
        }

        private async void UserMainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var viewModel = ((UserMainViewModel)Master.BindingContext);
            if (e.PropertyName == nameof(viewModel.IsLogoutSucceed) && viewModel.IsLogoutSucceed)
            {
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Task.Delay(200);
                await Navigation.PopAsync();
            }
        }

        private async void MainPage_Appearing(object sender, System.EventArgs e)
        {
            if (ServiceContext.Instance.CurrentUser == null)
            {
                if (Application.Current.Properties.ContainsKey("SessionId"))
                {
                    string sessionId = (string)Application.Current.Properties["SessionId"];
                    await CheckSession(sessionId);
                }
                else
                {
                    Navigation.InsertPageBefore(new LoginPage(), this);
                    await Task.Delay(200);
                    await Navigation.PopAsync();
                }
            }
        }

        private async Task CheckSession(string sessionId)
        {
            var result = await new UserService().CheckSession(sessionId);
            if (result.HasError)
            {
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Task.Delay(200);
                await Navigation.PopAsync(true);
            }
            else
            {
                ServiceContext.Instance.CurrentUser = result.Model;
            }
        }
    }
}
