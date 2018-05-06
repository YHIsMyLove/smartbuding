using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SmartConstructionSite.Core.Common;
using SmartConstructionSite.Core.Rankings.Models;

namespace SmartConstructionSite.Core.Rankings.ViewModels
{
    public class NormalRankingListViewModel : ViewModelBase
    {
        public NormalRankingListViewModel()
        {
            Rankings = new ObservableCollection<NormalRankingItem>(SimpleData.Instance.GetNormalRankingItems());
        }

        public ObservableCollection<NormalRankingItem> Rankings
        {
            get;
            private set;
        }
    }
}
