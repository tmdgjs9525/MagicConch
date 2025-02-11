using CommunityToolkit.Mvvm.Input;
using MagicConch.Core;
using MagicConch.Core.Navigate;
using MagicConch.Regions;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MagicConch.Views
{
    public partial class MainViewModel : ViewModelBase
    {
        #region fields

        private readonly INavigationService _navigationService;
        //api key 생성 후 nullable 경고 처리
        private readonly IChatCompletionService _chatCompletionService;

        #endregion

        #region properties

        #endregion
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
           // _chatCompletionService = chatCompletionService;
            
        }

        #region Commands
        [RelayCommand]
        private async Task test()
        {
            var chatHistory = new ChatHistory();
            chatHistory.AddSystemMessage("나는 스펀지밥에 나오는 마법의 소라고동이야 마법의 소라고동 처럼 대답 해줘");

            chatHistory.AddUserMessage("넌 누구야");

            var result = await _chatCompletionService.GetChatMessageContentsAsync(chatHistory);
        }
        #endregion
    }
}
