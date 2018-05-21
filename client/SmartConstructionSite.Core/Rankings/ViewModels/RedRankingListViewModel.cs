using System;
using System.Collections.ObjectModel;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.Rankings.Models;

namespace SmartConstructionSite.Core.Rankings.ViewModels
{
    public class RedRankingListViewModel : ViewModelBase
    {
        public RedRankingListViewModel()
        {
            Rankings = new ObservableCollection<RedRankingItem>(SimpleData.Instance.GetRedRankingItems());
        }

        public ObservableCollection<RedRankingItem> Rankings
        {
            get;
            private set;
        }
    }
}
