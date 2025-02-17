using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagicConch.Core;
using MagicConch.Core.Navigate;
using MagicConch.Extensions;
using MagicConch.Helper;
using MagicConch.Regions;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MagicConch.Views
{
    public partial class MainViewModel : ViewModelBase
    {
        #region fields
        private DispatcherTimer? _timer;
        //api key 생성 후 nullable 경고 처리
        private readonly IChatCompletionService _chatCompletionService;

        #endregion

        #region properties
        [ObservableProperty]
        private string _currentTime = string.Empty;

        [ObservableProperty]
        private string _countryName = string.Empty;

        [ObservableProperty]
        private bool _isMusicPlaying = false;
        #endregion
        public MainViewModel()
        {
            // _chatCompletionService = chatCompletionService;

            CountryName = CountryHelper.GetCurrentCountryNameInEnglish();

            refreshTimeZoneInfo();
            startTimeZoneMonitor();
        }

        #region Commands
        [RelayCommand]
        private async Task test()
        {
            var chatHistory = new ChatHistory();
            chatHistory.AddSystemMessage("너는 스펀지밥에 나오는 마법의 소라고동이야 마법의 소라고동 처럼 대답 해줘");

            chatHistory.AddUserMessage("넌 누구야");

            var result = await _chatCompletionService.GetChatMessageContentsAsync(chatHistory);
        }

        [RelayCommand]
        private void MusicPlay()
        {
            IsMusicPlaying = !IsMusicPlaying;
        }
        #endregion

        private void startTimeZoneMonitor()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1); // 1분마다 체크
            _timer.Tick += refreshTimeZoneInfo;
            _timer.Start();
        }

        private void refreshTimeZoneInfo(object? s = null, EventArgs? e = null)
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            DateTime localTime = DateTime.Now;
            localTime = TimeZoneInfo.ConvertTimeFromUtc(localTime.ToUniversalTime(), localTimeZone);

            string timeZoneAbbreviation = TimeZoneInfoHelper.GetTimeZoneAbbreviation(localTimeZone);

            CultureInfo enUS = new CultureInfo("en-US");
            string time = localTime.ToString("hh:mm tt", enUS) + $", {timeZoneAbbreviation}";

            CurrentTime = time;
        }
    }
}
