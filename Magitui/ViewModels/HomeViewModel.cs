using Magitui.Services.RepoContent;
using Magitui.Services.Storage;

namespace Magitui.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _gitHubUserName, _gitHubBranchName, _personalAccessToken, _gitHubRepositoryName;

        private ICommand _saveCommand;
        private bool _showCredentialsInput;
        private readonly IStorageService _storageService;
        private readonly IGitHubInfoService _gitHubInfoService;

        public HomeViewModel()
        {
            _storageService = ServicesProvider.GetService<IStorageService>();
            _gitHubInfoService = ServicesProvider.GetService<IGitHubInfoService>();
        }
        public ICommand SaveCommand => _saveCommand ??=
            new AsyncCommand(SaveCredentialsAsync);

        private async Task SaveCredentialsAsync()
        {
            var credentials = (PersonalAccessToken, GitHubUserName, GitHubBranchName, GitHubRepositoryName);
            await _storageService.SetGitHubCredentialsAsync(credentials);
            var gitHubInfo = await _gitHubInfoService.GetGitHubInfo(credentials);
            ShowCredentialsInput = gitHubInfo.IsAuthorized == false;
        }

        public string GitHubUserName
        {
            get => _gitHubUserName;
            set => SetProperty(ref _gitHubUserName, value);
        }

        public string GitHubRepositoryName
        {
            get => _gitHubRepositoryName;
            set => SetProperty(ref _gitHubRepositoryName, value);
        }

        public bool ShowCredentialsInput
        {
            get => _showCredentialsInput;
            set => SetProperty(ref _showCredentialsInput, value);
        }

        public string GitHubBranchName
        {
            get => _gitHubBranchName;
            set => SetProperty(ref _gitHubBranchName, value);
        }

        public async Task InitializeAsync()
        {
            var credentials = await _storageService.GetGitHubCredentialsAsync();
            if (credentials == default)
            {
                ShowCredentialsInput = true;
            }
        }

        public string PersonalAccessToken
        {
            get => _personalAccessToken;
            set => SetProperty(ref _personalAccessToken, value);
        }





    }
}
