using MagicConch.Core;
using MagicConch.Core.Navigate;
using MagicConch.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace MagicConch.Views
{
    public partial class MainPageViewModel : ViewModelBase
    {
        #region fields
        private readonly INavigationService _navigationService;
        #endregion

        #region properties

        #endregion
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            _navigationService.NavigateTo(RegionNames.MainRegion, ViewNames.MainView);
        }

        #region Commands

        #endregion
    }
}
