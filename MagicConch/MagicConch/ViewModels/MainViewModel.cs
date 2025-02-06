using MagicConch.Core;
using MagicConch.Core.Navigate;
using MagicConch.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace MagicConch.Views
{
    public partial class MainViewModel : ViewModelBase
    {
        #region fields
        private readonly INavigationService _navigationService;
        #endregion

        #region properties

        #endregion
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        #region Commands

        #endregion
    }
}
