using Magitui.Services.RepoContent;
using Magitui.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _gitHubUserName, _gitHubBranchName, _personalAccessToken;
        private ICommand _saveCommand;
        private readonly IStorageService _storageService;
        private readonly IRepoContentService _repoContentService;

        public HomeViewModel()
        {
            _storageService = ServicesProvider.GetService<IStorageService>();
            _repoContentService = ServicesProvider.GetService<IRepoContentService>();
        }
        public ICommand SaveCommand => _saveCommand ??=
            new AsyncCommand(SaveCredentialsAsync);

        private async Task SaveCredentialsAsync()
        {
            await _storageService.SetGitHubCredentialsAsync(PersonalAccessToken, GitHubUserName, GitHubBranchName);
        }

        public string GitHubUserName
        {
            get => _gitHubUserName;
            set => SetProperty(ref _gitHubUserName, value);
        }

        public string GitHubBranchName
        {
            get => _gitHubBranchName;
            set => SetProperty(ref _gitHubBranchName, value);
        }

        public async Task InitializeAsync()
        {
            var repoContent = await _repoContentService.GetRepositoryContentsClient();
        }

        public string PersonalAccessToken
        {
            get => _personalAccessToken;
            set => SetProperty(ref _personalAccessToken, value);
        }





    }
}
