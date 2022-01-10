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
        private string _gitHubUserName, _gitHubBranchName, _personalAccessToken, _gitHubRepositoryName;

        private ICommand _saveCommand;
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
            //var info = await _gitHubInfoService.GetGitHubInfo();
            await _storageService.SetGitHubCredentialsAsync(PersonalAccessToken, GitHubUserName, GitHubBranchName, GitHubRepositoryName);
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

        public string GitHubBranchName
        {
            get => _gitHubBranchName;
            set => SetProperty(ref _gitHubBranchName, value);
        }

        public async Task InitializeAsync()
        {
            //var ghInfo = await _gitHubInfoService.GetGitHubInfo();
            //var repoContent = ghInfo.Client;
        }

        public string PersonalAccessToken
        {
            get => _personalAccessToken;
            set => SetProperty(ref _personalAccessToken, value);
        }





    }
}
